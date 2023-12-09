using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calendar
{
    public partial class MainPage : ContentPage
    {
        public int[] Days { get; set; }
        public int Day = DateTime.Now.Day;
        public int Month = DateTime.Now.Month;
        public int Year = DateTime.Now.Year;
        public static string DOW = DateTime.Now.DayOfWeek.ToString();
        public int DayOfTheWeekNumber = GetNumOfDayOfWeek(DOW);

        //
        public int OnlyOneDay = DateTime.Now.Day;
        public int OnlyOneMonth = DateTime.Now.Month;
        public int OnlyOneYear = DateTime.Now.Year;
        public static string OnlyOneDOW = DateTime.Now.DayOfWeek.ToString();
        public int OnlyOneDayOfWeek = GetNumOfDayOfWeek(OnlyOneDOW);

        public MainPage()
        {
            InitializeComponent();
            CurrentDate.Text = $"{GetMonthOfNum(Month)} {Year}";

            int num = ThisFirstDay(DayOfTheWeekNumber);
            int days = CountDays(Month, Year);
            Days = MassiveOfDays(days);
            DisplayMonth(Days, num);
        }

        public int ThisFirstDay(int CurrDOW)
        {
            int firstDay;
            int one = 1;
            int today = DateTime.Now.Day;

            while (one < today)
            {
                one += 7;
            }
            firstDay = (CurrDOW + (one - today)) % 7;
            DayOfTheWeekNumber = firstDay;
            return firstDay;
        }

        public static int GetNumOfDayOfWeek(string CurrDayOfWeek)
        {
            int CurrDayOfTheWeek;
            switch (CurrDayOfWeek)
            {
                case "Monday":
                    return CurrDayOfTheWeek = 1;
                case "Tuesday":
                    return CurrDayOfTheWeek = 2;
                case "Wednesday":
                    return CurrDayOfTheWeek = 3;
                case "Thursday":
                    return CurrDayOfTheWeek = 4;
                case "Friday":
                    return CurrDayOfTheWeek = 5;
                case "Saturday":
                    return CurrDayOfTheWeek = 6;
                default:
                    return CurrDayOfTheWeek = 7;
            }
        }

        private int[] MassiveOfDays(int daysCount)
        {
            int[] DaysLenght = new int[42];

            for (int i = 1; i <= daysCount; i++)
                DaysLenght[i - 1] = i;
            for (int i = daysCount; i < 42; i++)
                DaysLenght[i] = 0;
            return DaysLenght;
        }

        private int CountDays(int month, int year)
        {
            int[] monthWwith31Days = { 1, 3, 5, 7, 8, 10, 12 };
            int[] monthWwith30Days = { 4, 6, 9, 11 };

            if (monthWwith31Days.Contains(month))
                return 31;
            else if (monthWwith30Days.Contains(month))
                return 30;
            else
            {
                if (year % 4 != 0)
                    return 28;
                else return 29;
            }
        }

        private void DisplayMonth(int[] result, int num)
        {
            int[] days = new int[] { };
            if (num != 1)
                days = InputZerosToDaysArray(result, num); // Заполняем в начале нулями, если начинаем не с пн
            else
                days = result;


            int row = 0;
            for (int j = 0; j < 6; j++)     // кол-во рядов
            {
                for (int i = 0; i < 7; i++) // кол-во столбиков
                {
                    Label dayLabel = new Label()
                    {
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        HeightRequest = 40,
                        FontSize = 15,
                        TextColor = Color.White
                    };

                    if (i == 5 || i == 6)
                        dayLabel.TextColor = Color.Red;
                    try
                    {
                        if (days[i + row] != 0)
                            dayLabel.Text = days[i + row].ToString();
                    }
                    catch (Exception e) { }


                    if (dayLabel.Text == DateTime.Now.Day.ToString() && Month == OnlyOneMonth && Year == OnlyOneYear)
                        dayLabel.BackgroundColor = Color.LightBlue;


                    Grid.SetColumn(dayLabel, i);
                    Grid.SetRow(dayLabel, j);
                    gridMonth.Children.Add(dayLabel);
                }
                row += 7;
            }
        }

        private int[] InputZerosToDaysArray(int[] days, int num)
        {
            if (num == 0)
                num = 7;

            int[] res = new int[days.Length];

            for (int i = 0; i < num - 1; i++)
                res[i] = 0;

            for (int i = num - 1; i < days.Length; i++)
                res[i] = days[i - num + 1];
            return res;
        }

        private string GetMonthOfNum(int CurrMonth)
        {
            switch (CurrMonth)
            {
                case 1:
                    return "Январь";
                case 2:
                    return "Февраль";
                case 3:
                    return "Март";
                case 4:
                    return "Апрель";
                case 5:
                    return "Май";
                case 6:
                    return "Июнь";
                case 7:
                    return "Июль";
                case 8:
                    return "Август";
                case 9:
                    return "Сентябрь";
                case 10:
                    return "Октябрь";
                case 11:
                    return "Ноябрь";
                default:
                    return "Декабрь";
            }
        }

        private void Left_Arrow_Clicked(object sender, EventArgs e)
        {
            int thisMonthDays = CountDays(Month, Year);
            Month = Month - 1;
            if (Month == 0)
            {
                Month = 12;
                Year--;
            }

            CurrentDate.Text = $"{GetMonthOfNum(Month)} {Year}";
            string where = "lastMonth";

            int daysCount = CountDays(Month, Year);
            int newNum = GetNewMonthDOW(where, daysCount, thisMonthDays);

            Days = MassiveOfDays(daysCount);
            gridMonth.Children.Clear();
            DisplayMonth(Days, newNum);

        }

        private void Right_Arrow_Clicked(object sender, EventArgs e)
        {
            int thisMonthDays = CountDays(Month, Year);
            Month = Month + 1;
            if (Month == 13)
            {
                Month = 1;
                Year++;
            }

            CurrentDate.Text = $"{GetMonthOfNum(Month)} {Year}";
            string where = "nextMonth";

            int daysCount = CountDays(Month, Year);
            int newNum = GetNewMonthDOW(where, daysCount, thisMonthDays);

            Days = MassiveOfDays(daysCount);
            gridMonth.Children.Clear();
            DisplayMonth(Days, newNum);

        }

        private int GetNewMonthDOW(string where, int daysCount, int thisMonthDaysCount)
        {
            if (where == "lastMonth")
            {
                if (daysCount == 31)
                {
                    DayOfTheWeekNumber = (DayOfTheWeekNumber + 4) % 7;
                    return DayOfTheWeekNumber;
                }
                if (daysCount == 30)
                {
                    DayOfTheWeekNumber = (DayOfTheWeekNumber + 5) % 7;
                    return DayOfTheWeekNumber;
                }

                if (daysCount == 29)
                {
                    DayOfTheWeekNumber = (DayOfTheWeekNumber + 6) % 7;
                    return DayOfTheWeekNumber;
                }
                else return DayOfTheWeekNumber;
            }
            else
            {
                if (thisMonthDaysCount == 31)
                {
                    DayOfTheWeekNumber = (DayOfTheWeekNumber + 3) % 7;
                    return DayOfTheWeekNumber;
                }

                if (thisMonthDaysCount == 30)
                {
                    DayOfTheWeekNumber = (DayOfTheWeekNumber + 2) % 7;
                    return DayOfTheWeekNumber;
                }

                if (thisMonthDaysCount == 29)
                {
                    DayOfTheWeekNumber = (DayOfTheWeekNumber + 1) % 7;
                    return DayOfTheWeekNumber;
                }
                else
                    return DayOfTheWeekNumber;
            }
        }

        private void ShowThisMonth_Clicked(object sender, EventArgs e)
        {
            CurrentDate.Text = $"{GetMonthOfNum(OnlyOneMonth)} {OnlyOneYear}";
            Month = OnlyOneMonth;
            Year = OnlyOneYear;
            int numOfFirstDayOfWeek = ThisFirstDay(OnlyOneDayOfWeek);
            int daysCount = CountDays(OnlyOneMonth, OnlyOneYear);
            Days = MassiveOfDays(daysCount);
            gridMonth.Children.Clear();
            DisplayMonth(Days, numOfFirstDayOfWeek);
        }
    }
}
