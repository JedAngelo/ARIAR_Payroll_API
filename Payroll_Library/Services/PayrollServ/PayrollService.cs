using Microsoft.EntityFrameworkCore;
using Payroll_Library.Models;
using Payroll_Library.Models.Dto;
using Payroll_Library.Models.Dto.PayrollDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll_Library.Services.PayrollServ
{
    public class PayrollService
    {

        #region Payroll Constants
		/*-------------------------------------------------------------------------------*/

        //--- Philhealth Constants ---
        public const decimal PHILHEALTH_PREMIUM_RATE = 0.05m;
        public const decimal PHILHEALTH_INCOME_FLOOR = 10000m;
        public const decimal PHILHEALTH_INCOME_CEILING = 100000m;

        //--- Pag-IBIG Constants ---
        public const decimal PAGIBIG_EMPLOYER_RATE = 0.02m;
        public const decimal PAGIBIG_EMPLOYEE_RATE = 0.02m;
        public const decimal PAGIBIG_MONTHLY_COMPENSATION_CEILING = 10000m;
        public const decimal PAGIBIG_MAX_EMPLOYER_CONTRIBUTION = 200m;
        public const decimal PAGIBIG_MAX_EMPLOYEE_CONTRIBUTION = 200m;

		/*-------------------------------------------------------------------------------*/
        #endregion

        private readonly AriarPayrollDbContext _context;

        public PayrollService(AriarPayrollDbContext context)
        {
            _context = context;
        }

		public async Task<ApiResponse<string>> CalculatePayroll(DateOnly startDate, DateOnly endDate)
        {
			try
			{
				var _settings = await _context.SystemSettings.FirstOrDefaultAsync(x => x.SettingsId == 1);
				var _employees = await _context.PersonalInformations.Where(x => x.IsActive).Include(d => d.EmploymentDetail).Include(p => p.Payrolls).ToListAsync();
				var _attendances = await _context.Attendances.Where(a => a.AttendanceDate >= startDate && a.AttendanceDate <= endDate).ToListAsync();
				var _day = endDate.Day;
				var payrolls = new List<Payroll>();


                foreach (var employee in _employees)
				{
					var empAttendance = _attendances.Where(e => e.PersonalId == employee.PersonalId);

					var empPayrolls = employee.Payrolls.FirstOrDefault
									  (x => x.PersonalId == employee.PersonalId &&
									  (x.StartDate == startDate && x.EndDate == endDate));

					var empEmploymentDetails = employee.EmploymentDetail!;
					
					var empLateDeductions = 0m;


                    
                    //Total work hours
                    decimal workedHours = 0.0m;
					//Gross salary
                    var grossSalary = 0m;

					foreach (var attendance in empAttendance)
					{
						var payRateRaw = empEmploymentDetails.PayRate;
                        var empHourlyRateRaw = payRateRaw / 8;
                        var empMinuteRateRaw = empHourlyRateRaw / 60;

                        var payRate = payRateRaw * attendance.PayMultiplier;
                        var empHourlyRate = payRate / 8;
                        var empMinuteRate = empHourlyRate / 60;

						var _earlyOutEndsMorning = _settings!.EarlyOutEndsMorning;
						var _earlyOutEndsAfternoon = _settings!.EarlyOutEndsAfternoon;
						//grossSalary += workedHours * empHourlyRate;

                        //Morning late calculation
						if (attendance.MorningIn > _settings!.LateStartTimeMorning)
						{
							var minuteLate = attendance.MorningIn - _settings!.LateStartTimeMorning;
							empLateDeductions += CalculateLateDeductions((TimeSpan)minuteLate, empMinuteRateRaw);
						}

						

						if (attendance.Status == "OUT")
						{
							var start = attendance.MorningIn != null ? new TimeOnly(8,0,0) : new TimeOnly(13,0,0);
							if (attendance.AfternoonOut != null)
							{
								var end = new TimeOnly(17, 0, 0);
								if (end < _earlyOutEndsAfternoon)
								{
									end = (TimeOnly)attendance.AfternoonOut;
								}
								workedHours += CalculateHours(start, end);
							}					
							else if (attendance.MorningOut != null)
							{
								var end = new TimeOnly(12, 0, 0);
								if (end < _earlyOutEndsMorning)
								{
									end = (TimeOnly)attendance.MorningOut;
                                }
                                workedHours += CalculateHours(start, end);
                            }							

						}
						else if (attendance.Status == "TIME OUT(PM)")
						{
							var partialHours = 0m;
							if (attendance.MorningIn != null && attendance.MorningOut != null)
							{
								var start = new TimeOnly(8, 0, 0);
								var end = new TimeOnly(12,0,0);
								if (attendance.MorningOut < _earlyOutEndsMorning)
								{
									end = (TimeOnly)attendance.MorningOut;
								}
								partialHours += CalculateHours(start, end);
							}
							if (attendance.AfternoonIn != null && attendance.AfternoonOut != null)
							{
								var start = new TimeOnly(13, 0, 0);
								var end = new TimeOnly(17, 0, 0);
								if (attendance.AfternoonOut < _earlyOutEndsAfternoon)
								{
									end = (TimeOnly)attendance.AfternoonOut;
								}
								partialHours += CalculateHours(start, end);
							}
							workedHours += partialHours;
                        }
						else if (attendance.Status == "TIME OUT(AM)")
						{
							if (attendance.MorningIn != null && attendance.MorningOut != null)
							{
								var start = new TimeOnly(8, 0, 0);
								var end = new TimeOnly(13, 0, 0);
								if (attendance.MorningOut < _earlyOutEndsMorning)
								{
									end = (TimeOnly)attendance.MorningOut;
								}
								workedHours += CalculateHours(start, end);
							}
						}
						else if (attendance.Status == "LEAVE")
						{
							workedHours += 8.0m;
						}

						grossSalary += workedHours * empHourlyRate;

                    }
					var totalWorkedDays = workedHours / 8m;


					//Deductions
					//To do: Add real calculation with methods
					var otherDeductions = 0m;
					var commissions = 0m;					
					var employerSssShare = CalculateSSSEmployerContribution(grossSalary);
					var employeeSssShare = CalculateSSSEmployeeContribution(grossSalary);
					var employerPagibigShare = CalculatePagIbigEmployerContribution(grossSalary);
					var employeePagibigShare = CalculatePagIbigEmployeeContribution(grossSalary);
					var employerPhilhealthShare = CalculatePhilHealthContribution(grossSalary);
					var employeePhilhealthShare = CalculatePhilHealthContribution(grossSalary);
					var totalDeductions = 0m;
                    if (_day == 15)
                    {
                        totalDeductions = employeePhilhealthShare + employeePagibigShare + otherDeductions;
                    }
                    else if (_day == 30)
                    {
                        totalDeductions = employeeSssShare + otherDeductions;
                    }


					var incomeTax = empEmploymentDetails.IncomeTaxRate;

                    //Net salary
                    var netSalary = (grossSalary - totalDeductions) + commissions;

                    var _existingPayroll = await _context.Payrolls.FirstOrDefaultAsync(x => x.StartDate == startDate && x.EndDate == endDate && x.PersonalId == employee.PersonalId);
                    if (_existingPayroll != null)
                    {
                        otherDeductions = _existingPayroll.OtherDeductions;
                        commissions = _existingPayroll.Commissions;
                        
                        netSalary = (_existingPayroll.NetSalary - otherDeductions) + commissions;

                        //Update existing payroll
                        _existingPayroll.NetSalary = netSalary;
                    }
					else
					{
                        var _payroll = new Payroll
                        {
                            PayrollId = new Guid(),
                            PersonalId = employee.PersonalId,
                            GrossSalary = grossSalary,
                            StartDate = startDate,
                            EndDate = endDate,
                            NetSalary = netSalary,
                            TotalWorkDay = totalWorkedDays,
                            EmployeePagibigShare = employeePagibigShare,
                            EmployerPagibigShare = employerPagibigShare,
                            EmployeePhilhealthShare = employeePhilhealthShare,
                            EmployerPhilhealthShare = employerPhilhealthShare,
                            EmployeeSssShare = employeeSssShare,
                            EmployerSssShare = employerSssShare,
                            Commissions = commissions,
                            OtherDeductions = otherDeductions
                        };
                        await _context.Payrolls.AddAsync(_payroll);
                    }

                    await _context.SaveChangesAsync();
                    
				}




				return new ApiResponse<string>
				{
					Data = $"Payroll successfully calculated for date: {startDate.ToString()} to {endDate.ToString()}",
					ErrorMessage = "",
					IsSuccess = true
				};
			}
			catch (Exception ex)
			{
				return new ApiResponse<string>
				{
					Data = "",
					ErrorMessage = $"Error: {ex.Message}",
					IsSuccess = false
				};
			}
        }

		public async Task<ApiResponse<string>> AddOthers(PayrollDto dto)
		{
			try
			{
				var _existingPayroll = await _context.Payrolls.FirstOrDefaultAsync(x => x.PayrollId == dto.PayrollId) 
                                       ?? throw new KeyNotFoundException($"Payroll record not found with ID: '{dto.PayrollId}'");
                _existingPayroll.OtherDeductions = dto.OtherDeductions;
				_existingPayroll.Commissions = dto.Commissions;

				await _context.SaveChangesAsync();

				return new ApiResponse<string>
				{
					Data = "Other deduction/commissions added successfully",
					ErrorMessage = "",
					IsSuccess = true
				};

			}
			catch (Exception ex)
			{
                return new ApiResponse<string>
                {
                    Data = "",
                    ErrorMessage = $"Error: {ex.Message}",
                    IsSuccess = false
                };
            }
		}


		private decimal CalculateDailyWork(Attendance attendance)
		{
			decimal workedDays = 0.0m;
			bool fullDay = attendance.MorningIn != null && attendance.AfternoonOut != null;
			bool halfDay = (attendance.MorningIn != null && attendance.MorningOut != null) ||
						   (attendance.AfternoonIn != null && attendance.AfternoonOut != null);

			if (fullDay)
			{
				workedDays = 1.0m;
			}
			else if (halfDay)
			{
				workedDays = 0.5m;
			}

			return workedDays;
		}

		private decimal CalculateWorkHours(Attendance attendance)
		{
			if (attendance.MorningIn == null) return 0;
			if (attendance.MorningOut == null && attendance.AfternoonOut == null) return 0;
			

			var logOut = attendance.AfternoonOut ?? attendance.MorningOut;

			var result = (TimeOnly)logOut! - (TimeOnly)attendance.MorningIn;

			if (attendance.AfternoonOut != null) return (decimal)result.TotalHours;

            return (decimal)result.TotalHours;
		}

		private decimal CalculateHours(TimeOnly start, TimeOnly end)
		{
			var result = end - start;
			return (decimal)result.TotalHours;
		}

		private decimal CalculateLateDeductions(TimeSpan time, decimal minuteRate)
		{
			return (decimal)time.TotalMinutes * minuteRate;
		}



		private decimal CalculatePhilHealthContribution(decimal biMonthlyGrossSalary)
        {
            decimal monthlySalary = biMonthlyGrossSalary * 2;
            decimal taxableSalary = monthlySalary;

            if (monthlySalary < PHILHEALTH_INCOME_FLOOR)
            {
                taxableSalary = PHILHEALTH_INCOME_FLOOR;
            }
            else
            {
                taxableSalary = Math.Min(monthlySalary, PHILHEALTH_INCOME_CEILING);
            }

            decimal totalPremium = taxableSalary * PHILHEALTH_PREMIUM_RATE;
            decimal philhealthContribution = totalPremium / 2;

            return philhealthContribution;
        }

        private decimal CalculatePagIbigEmployerContribution(decimal biMonthlyGrossSalary)
        {
            decimal monthlySalary = biMonthlyGrossSalary * 2;
            decimal pagIbigEmployerContribution;

            if (monthlySalary > PAGIBIG_MONTHLY_COMPENSATION_CEILING)
            {
                pagIbigEmployerContribution = PAGIBIG_MAX_EMPLOYER_CONTRIBUTION;
            }
            else
            {
                pagIbigEmployerContribution = monthlySalary * PAGIBIG_EMPLOYER_RATE;
            }

            return pagIbigEmployerContribution;
        }

        private decimal CalculatePagIbigEmployeeContribution(decimal biMonthlyGrossSalary)
        {
            decimal monthlySalary = biMonthlyGrossSalary * 2;
            decimal pagIbigEmployeeContribution;

            if (monthlySalary > PAGIBIG_MONTHLY_COMPENSATION_CEILING)
            {
                pagIbigEmployeeContribution = PAGIBIG_MAX_EMPLOYEE_CONTRIBUTION;
            }
            else
            {
                pagIbigEmployeeContribution = monthlySalary * PAGIBIG_EMPLOYEE_RATE;
            }

            return pagIbigEmployeeContribution;
        }


        public static decimal CalculateSSSEmployeeContribution(decimal biMonthlyGrossSalary)
        {
            decimal monthlySalary = biMonthlyGrossSalary * 2;
            decimal sssEmployeeContribution = 0;
            foreach (var sssBracket in SssTable)
            {
                if (monthlySalary >= sssBracket.LowerLimit && monthlySalary <= sssBracket.UpperLimit)
                {
                    sssEmployeeContribution = sssBracket.EmployeeShare;
                    break;
                }
            }

            return sssEmployeeContribution;
        }


        public static decimal CalculateSSSEmployerContribution(decimal biMonthlyGrossSalary)
        {
            decimal monthlySalary = biMonthlyGrossSalary * 2;
            decimal sssEmployerContribution = 0;
            foreach (var sssBracket in SssTable)
            {
                if (monthlySalary >= sssBracket.LowerLimit && monthlySalary <= sssBracket.UpperLimit)
                {
                    sssEmployerContribution = sssBracket.EmployerShare;
                    break;
                }
            }

            return sssEmployerContribution;
        }
        private static List<SssMonthlyCredit> SssTable { get; } = new List<SssMonthlyCredit>()
        {
			new SssMonthlyCredit { Id = 1, LowerLimit = 0, UpperLimit = 4249.99m, MonthlySalaryCredit = 4000, EmployeeShare = 180.00m, EmployerShare = 380.00m + 10.00m },
            new SssMonthlyCredit { Id = 2, LowerLimit = 4250, UpperLimit = 4749.99m, MonthlySalaryCredit = 4500, EmployeeShare = 202.50m, EmployerShare = 427.50m + 10.00m },
			new SssMonthlyCredit { Id = 3, LowerLimit = 4750, UpperLimit = 5249.99m, MonthlySalaryCredit = 5000, EmployeeShare = 225.00m, EmployerShare = 475.00m + 10.00m },
            new SssMonthlyCredit { Id = 4, LowerLimit = 5250, UpperLimit = 5749.99m, MonthlySalaryCredit = 5500, EmployeeShare = 247.50m, EmployerShare = 522.50m + 10.00m },
            new SssMonthlyCredit { Id = 5, LowerLimit = 5750, UpperLimit = 6249.99m, MonthlySalaryCredit = 6000, EmployeeShare = 270.00m, EmployerShare = 570.00m + 10.00m },
            new SssMonthlyCredit { Id = 6, LowerLimit = 6250, UpperLimit = 6749.99m, MonthlySalaryCredit = 6500, EmployeeShare = 292.50m, EmployerShare = 617.50m + 10.00m },
            new SssMonthlyCredit { Id = 7, LowerLimit = 6750, UpperLimit = 7249.99m, MonthlySalaryCredit = 7000, EmployeeShare = 315.00m, EmployerShare = 665.00m + 10.00m },
            new SssMonthlyCredit { Id = 8, LowerLimit = 7250, UpperLimit = 7749.99m, MonthlySalaryCredit = 7500, EmployeeShare = 337.50m, EmployerShare = 712.50m + 10.00m },
            new SssMonthlyCredit { Id = 9, LowerLimit = 7750, UpperLimit = 8249.99m, MonthlySalaryCredit = 8000, EmployeeShare = 360.00m, EmployerShare = 760.00m + 10.00m },
            new SssMonthlyCredit { Id = 10, LowerLimit = 8250, UpperLimit = 8749.99m, MonthlySalaryCredit = 8500, EmployeeShare = 382.50m, EmployerShare = 807.50m + 10.00m },
            new SssMonthlyCredit { Id = 11, LowerLimit = 8750, UpperLimit = 9249.99m, MonthlySalaryCredit = 9000, EmployeeShare = 405.00m, EmployerShare = 855.00m + 10.00m },
            new SssMonthlyCredit { Id = 12, LowerLimit = 9250, UpperLimit = 9749.99m, MonthlySalaryCredit = 9500, EmployeeShare = 427.50m, EmployerShare = 902.50m + 10.00m },
            new SssMonthlyCredit { Id = 13, LowerLimit = 9750, UpperLimit = 10249.99m, MonthlySalaryCredit = 10000, EmployeeShare = 450.00m, EmployerShare = 950.00m + 10.00m },
            new SssMonthlyCredit { Id = 14, LowerLimit = 10250, UpperLimit = 10749.99m, MonthlySalaryCredit = 10500, EmployeeShare = 472.50m, EmployerShare = 997.50m + 10.00m },
            new SssMonthlyCredit { Id = 15, LowerLimit = 10750, UpperLimit = 11249.99m, MonthlySalaryCredit = 11000, EmployeeShare = 495.00m, EmployerShare = 1045.00m + 10.00m },
            new SssMonthlyCredit { Id = 16, LowerLimit = 11250, UpperLimit = 11749.99m, MonthlySalaryCredit = 11500, EmployeeShare = 517.50m, EmployerShare = 1092.50m + 10.00m },
            new SssMonthlyCredit { Id = 17, LowerLimit = 11750, UpperLimit = 12249.99m, MonthlySalaryCredit = 12000, EmployeeShare = 540.00m, EmployerShare = 1140.00m + 10.00m },
            new SssMonthlyCredit { Id = 18, LowerLimit = 12250, UpperLimit = 12749.99m, MonthlySalaryCredit = 12500, EmployeeShare = 562.50m, EmployerShare = 1187.50m + 10.00m },
            new SssMonthlyCredit { Id = 19, LowerLimit = 12750, UpperLimit = 13249.99m, MonthlySalaryCredit = 13000, EmployeeShare = 585.00m, EmployerShare = 1235.00m + 10.00m },
            new SssMonthlyCredit { Id = 20, LowerLimit = 13250, UpperLimit = 13749.99m, MonthlySalaryCredit = 13500, EmployeeShare = 607.50m, EmployerShare = 1282.50m + 10.00m },
            new SssMonthlyCredit { Id = 21, LowerLimit = 13750, UpperLimit = 14249.99m, MonthlySalaryCredit = 14000, EmployeeShare = 630.00m, EmployerShare = 1330.00m + 10.00m },
            new SssMonthlyCredit { Id = 22, LowerLimit = 14250, UpperLimit = 14749.99m, MonthlySalaryCredit = 14500, EmployeeShare = 652.50m, EmployerShare = 1377.50m + 10.00m },
            new SssMonthlyCredit { Id = 23, LowerLimit = 14750, UpperLimit = 15249.99m, MonthlySalaryCredit = 15000, EmployeeShare = 675.00m, EmployerShare = 1425.00m + 30.00m },
            new SssMonthlyCredit { Id = 24, LowerLimit = 15250, UpperLimit = 15749.99m, MonthlySalaryCredit = 15500, EmployeeShare = 697.50m, EmployerShare = 1472.50m + 30.00m },
            new SssMonthlyCredit { Id = 25, LowerLimit = 15750, UpperLimit = 16249.99m, MonthlySalaryCredit = 16000, EmployeeShare = 720.00m, EmployerShare = 1520.00m + 30.00m },
            new SssMonthlyCredit { Id = 26, LowerLimit = 16250, UpperLimit = 16749.99m, MonthlySalaryCredit = 16500, EmployeeShare = 742.50m, EmployerShare = 1567.50m + 30.00m },
            new SssMonthlyCredit { Id = 27, LowerLimit = 16750, UpperLimit = 17249.99m, MonthlySalaryCredit = 17000, EmployeeShare = 765.00m, EmployerShare = 1615.00m + 30.00m },
            new SssMonthlyCredit { Id = 28, LowerLimit = 17250, UpperLimit = 17749.99m, MonthlySalaryCredit = 17500, EmployeeShare = 787.50m, EmployerShare = 1662.50m + 30.00m },
			new SssMonthlyCredit { Id = 29, LowerLimit = 17750, UpperLimit = 18249.99m, MonthlySalaryCredit = 18000, EmployeeShare = 810.00m, EmployerShare = 1710.00m + 30.00m },
			new SssMonthlyCredit { Id = 30, LowerLimit = 18250, UpperLimit = 18749.99m, MonthlySalaryCredit = 18500, EmployeeShare = 832.50m, EmployerShare = 1757.50m + 30.00m },
            new SssMonthlyCredit { Id = 31, LowerLimit = 18750, UpperLimit = 19249.99m, MonthlySalaryCredit = 19000, EmployeeShare = 855.00m, EmployerShare = 1805.00m + 30.00m },
            new SssMonthlyCredit { Id = 32, LowerLimit = 19250, UpperLimit = 19749.99m, MonthlySalaryCredit = 19500, EmployeeShare = 877.50m, EmployerShare = 1852.50m + 30.00m },
            new SssMonthlyCredit { Id = 33, LowerLimit = 19750, UpperLimit = 20249.99m, MonthlySalaryCredit = 20000, EmployeeShare = 900.00m, EmployerShare = 1930.00m + 30.00m },
            new SssMonthlyCredit { Id = 34, LowerLimit = 20250, UpperLimit = 20749.99m, MonthlySalaryCredit = 20500, EmployeeShare = 922.50m, EmployerShare = 1930.00m + 70.00m},
            new SssMonthlyCredit { Id = 35, LowerLimit = 20750, UpperLimit = 21249.99m, MonthlySalaryCredit = 21000, EmployeeShare = 945.00m, EmployerShare = 1930.00m + 140.00m },
            new SssMonthlyCredit { Id = 36, LowerLimit = 21250, UpperLimit = 21749.99m, MonthlySalaryCredit = 21500, EmployeeShare = 967.50m, EmployerShare = 1930.00m + 210.00m },
            new SssMonthlyCredit { Id = 37, LowerLimit = 21750, UpperLimit = 22249.99m, MonthlySalaryCredit = 22000, EmployeeShare = 990.00m, EmployerShare = 1930.00m + 280.00m },
            new SssMonthlyCredit { Id = 38, LowerLimit = 22250, UpperLimit = 22749.99m, MonthlySalaryCredit = 22500, EmployeeShare = 1012.50m, EmployerShare = 1930.00m + 350.00m },
            new SssMonthlyCredit { Id = 39, LowerLimit = 22750, UpperLimit = 23249.99m, MonthlySalaryCredit = 23000, EmployeeShare = 1035.00m, EmployerShare = 1930.00m + 420.00m },
            new SssMonthlyCredit { Id = 40, LowerLimit = 23250, UpperLimit = 23749.99m, MonthlySalaryCredit = 23500, EmployeeShare = 1057.50m, EmployerShare = 1930.00m + 490.00m },
            new SssMonthlyCredit { Id = 41, LowerLimit = 23750, UpperLimit = 24249.99m, MonthlySalaryCredit = 24000, EmployeeShare = 1080.00m, EmployerShare = 1930.00m + 560.00m },
            new SssMonthlyCredit { Id = 42, LowerLimit = 24250, UpperLimit = 24749.99m, MonthlySalaryCredit = 24500, EmployeeShare = 1102.50m, EmployerShare = 1930.00m + 630.00m },
            new SssMonthlyCredit { Id = 43, LowerLimit = 24750, UpperLimit = 25249.99m, MonthlySalaryCredit = 25000, EmployeeShare = 1125.00m, EmployerShare = 1930.00m + 700.00m },
            new SssMonthlyCredit { Id = 44, LowerLimit = 25250, UpperLimit = 25749.99m, MonthlySalaryCredit = 25500, EmployeeShare = 1147.50m, EmployerShare = 1930.00m + 770.00m },
            new SssMonthlyCredit { Id = 45, LowerLimit = 25750, UpperLimit = 26249.99m, MonthlySalaryCredit = 26000, EmployeeShare = 1170.00m, EmployerShare = 1930.00m + 840.00m },
            new SssMonthlyCredit { Id = 46, LowerLimit = 26250, UpperLimit = 26749.99m, MonthlySalaryCredit = 26500, EmployeeShare = 1192.50m, EmployerShare = 1930.00m + 910.00m },
            new SssMonthlyCredit { Id = 47, LowerLimit = 26750, UpperLimit = 27249.99m, MonthlySalaryCredit = 27000, EmployeeShare = 1215.00m, EmployerShare = 1930.00m + 980.00m },
			new SssMonthlyCredit { Id = 48, LowerLimit = 27250, UpperLimit = 27749.99m, MonthlySalaryCredit = 27500, EmployeeShare = 1237.50m, EmployerShare = 1930.00m + 1050.00m },
            new SssMonthlyCredit { Id = 49, LowerLimit = 27750, UpperLimit = 28249.99m, MonthlySalaryCredit = 28000, EmployeeShare = 1260.00m, EmployerShare = 1930.00m + 1120.00m },
			new SssMonthlyCredit { Id = 50, LowerLimit = 28250, UpperLimit = 28749.99m, MonthlySalaryCredit = 28500, EmployeeShare = 1282.50m, EmployerShare = 1930.00m + 1190.00m },
            new SssMonthlyCredit { Id = 51, LowerLimit = 28750, UpperLimit = 29249.99m, MonthlySalaryCredit = 29000, EmployeeShare = 1305.00m, EmployerShare = 1930.00m + 1260.00m },
            new SssMonthlyCredit { Id = 52, LowerLimit = 29250, UpperLimit = decimal.MaxValue, MonthlySalaryCredit = 30000, EmployeeShare = 1350.00m, EmployerShare = 1930.00m + 1400.00m }
        };

    }
}
