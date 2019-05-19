using Microsoft.EntityFrameworkCore;
using Platform.Data;
using Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Migrations.Seed
{
	public static class DBInitializer
	{
		private static bool _initialized = false;
		private static object _lock = new object();
		private static List<TroopType> troopTypes;
		private static List<TroopKind> troopKinds;
		private static List<RecruitmentOffice> recruitmentOffices;

		public static void Seed(DataContext context)
		{
			AddTroopKinds(context);
			AddTroopTypes(context);
			AddRecruitmentOffices(context);
		}

		internal static void Initialize(DataContext context)
		{
			if (!_initialized)
			{
				lock (_lock)
				{
					if (_initialized)
						return;

					InitializeData(context);
				}
			}
		}

		private static void AddTroopKinds(DataContext context)
		{
			troopKinds = new List<TroopKind>
			{
				new TroopKind()
				{
					Name = "Сухопутные войска"
				},
				new TroopKind()
				{
					Name = "Военно-морской флот"
				},
				new TroopKind()
				{
					Name = "Военно-воздушные силы"
				},
				new TroopKind()
				{
					Name = "Войска противовоздушной обороны страны"
				},
				new TroopKind()
				{
					Name = "Ракетные войска стратегического назначения"
				},
				new TroopKind()
				{
					Name = "Отдельные рода войск"
				},
				new TroopKind()
				{
					Name = "Специальные войска ВС (боевого обеспечения)"
				},
				new TroopKind()
				{
					Name = "Специальные войска тыла (ВС)"
				}
			};
			if (!context.TroopKinds.Any())
			{
				context.TroopKinds.AddRange(troopKinds);
				context.SaveChanges();
			}
		}

		private static void AddTroopTypes(DataContext context)
		{
			troopTypes = new List<TroopType>
			{
				new TroopType()
				{
					Name = "Мотострелковые войска",
					TroopKindId = 1
				},
				new TroopType()
				{
					Name = "Танковые войска",
					TroopKindId = 1
				},
				new TroopType()
				{
					Name = "Ракетные войска и Артиллерия",
					TroopKindId = 1
				},
				new TroopType()
				{
					Name = "Войска противовоздушной обороны СВ",
					TroopKindId = 1
				},
				new TroopType()
				{
					Name = "Десантно-штурмовые формирования",
					TroopKindId = 1
				},
				new TroopType()
				{
					Name = "Специальные войска и тыл",
					TroopKindId = 1
				},
				new TroopType()
				{
					Name = "Надводные силы",
					TroopKindId = 2
				},
				new TroopType()
				{
					Name = "Подводные силы",
					TroopKindId = 2
				},
				new TroopType()
				{
					Name = "Морская авиация (ВМС ВМФ)",
					TroopKindId = 2
				},
				new TroopType()
				{
					Name = "Береговые ракетно-артиллерийские войска",
					TroopKindId = 2
				},
				new TroopType()
				{
					Name = "Морская пехота",
					TroopKindId = 2
				},
				new TroopType()
				{
					Name = "Специальные войска и тыл",
					TroopKindId = 2
				},
				new TroopType()
				{
					Name = "Фронтовая авиация",
					TroopKindId = 3
				},
				new TroopType()
				{
					Name = "Дальняя авиация",
					TroopKindId = 3
				},
				new TroopType()
				{
					Name = "Военно-транспортная авиация",
					TroopKindId = 3
				},
				new TroopType()
				{
					Name = "Армейская авиация",
					TroopKindId = 3
				},
				new TroopType()
				{
					Name = "Специальные войска и тыл",
					TroopKindId = 3
				},
				new TroopType()
				{
					Name = "Истребительная авиация ПВО",
					TroopKindId = 4
				},
				new TroopType()
				{
					Name = "Зенитные ракетные войска",
					TroopKindId = 4
				},
				new TroopType()
				{
					Name = "Радиотехнические войска",
					TroopKindId = 4
				},
				new TroopType()
				{
					Name = "Специальные войска и тыл",
					TroopKindId = 4
				},
				new TroopType()
				{
					Name = "Ракетные войска стратегического назначения",
					TroopKindId = 5
				},
				new TroopType()
				{
					Name = "Воздушно-десантные войска",
					TroopKindId = 6
				},
				new TroopType()
				{
					Name = "Военно-космические войска",
					TroopKindId = 6
				},
				new TroopType()
				{
					Name = "Инженерные войска",
					TroopKindId = 7
				},
				new TroopType()
				{
					Name = "Войска связи",
					TroopKindId = 7
				},
				new TroopType()
				{
					Name = "Химические войска",
					TroopKindId = 7
				},
				new TroopType()
				{
					Name = "Радиотехнические войска. Войска РЭБ",
					TroopKindId = 7
				},
				new TroopType()
				{
					Name = "Военно-строительные части",
					TroopKindId = 8
				},
				new TroopType()
				{
					Name = "Автомобильные войска",
					TroopKindId = 8
				},
				new TroopType()
				{
					Name = "Дорожно-строительные войска",
					TroopKindId = 8
				},
				new TroopType()
				{
					Name = "Железнодорожные войска",
					TroopKindId = 8
				},
				new TroopType()
				{
					Name = "Трубопроводные войска",
					TroopKindId = 8
				}
			};
			if (!context.TroopTypes.Any())
			{
				context.TroopTypes.AddRange(troopTypes);
				context.SaveChanges();
			}
		}

		private static void AddRecruitmentOffices(DataContext context)
		{
			recruitmentOffices = new List<RecruitmentOffice>
			{
				new RecruitmentOffice()
				{
					Address = "г. Ульяновск, ул. Автозаводская, д. 8",
					ChiefFullName = "Петренко Влад Олегович",
					OfficeNo = "433-033-111",
					Schedule = new List<Schedule>()
					{
						new Schedule()
						{
							DayOfWeek = DayOfWeek.Monday,
							StartDate = "8.00",
							EndDate = "17.00",
						},
						new Schedule()
						{
							DayOfWeek = DayOfWeek.Tuesday,
							StartDate = "8.00",
							EndDate = "17.00",
						},
						new Schedule()
						{
							DayOfWeek = DayOfWeek.Wednesday,
							StartDate = "8.00",
							EndDate = "17.00",
						},
						new Schedule()
						{
							DayOfWeek = DayOfWeek.Thursday,
							StartDate = "8.00",
							EndDate = "17.00",
						},
						new Schedule()
						{
							DayOfWeek = DayOfWeek.Friday,
							StartDate = "8.00",
							EndDate = "17.00",
						},
						new Schedule()
						{
							DayOfWeek = DayOfWeek.Saturday,
							StartDate = "8.00",
							EndDate = "13.00",
						},
						new Schedule()
						{
							DayOfWeek = DayOfWeek.Sunday,
							StartDate = "8.00",
							EndDate = "13.00",
						},
					}
				},
				new RecruitmentOffice()
				{
					Address = "г. Ульяновск, ул. Новогородняя, д. 8",
					ChiefFullName = "Петренко Петр Олегович",
					OfficeNo = "433-033-112",
					Schedule = new List<Schedule>()
					{
						new Schedule()
						{
							DayOfWeek = DayOfWeek.Monday,
							StartDate = "8.00",
							EndDate = "17.00",
						},
						new Schedule()
						{
							DayOfWeek = DayOfWeek.Tuesday,
							StartDate = "8.00",
							EndDate = "17.00",
						},
						new Schedule()
						{
							DayOfWeek = DayOfWeek.Wednesday,
							StartDate = "8.00",
							EndDate = "17.00",
						},
						new Schedule()
						{
							DayOfWeek = DayOfWeek.Thursday,
							StartDate = "8.00",
							EndDate = "17.00",
						},
						new Schedule()
						{
							DayOfWeek = DayOfWeek.Friday,
							StartDate = "8.00",
							EndDate = "17.00",
						},
						new Schedule()
						{
							DayOfWeek = DayOfWeek.Saturday,
							StartDate = "8.00",
							EndDate = "13.00",
						},
						new Schedule()
						{
							DayOfWeek = DayOfWeek.Sunday,
							StartDate = "8.00",
							EndDate = "13.00",
						},
					}
				}
			};
			if (!context.RecruitmentOffices.Any())
			{
				context.RecruitmentOffices.AddRange(recruitmentOffices);
				context.SaveChanges();
			}
		}

		private static void InitializeData(DataContext context)
		{
			context.Database.Migrate();
			Seed(context);
		}
	}
}
