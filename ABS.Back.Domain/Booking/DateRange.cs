﻿namespace ABS.Back.Domain.Booking
{
    public record DateRange
    {
        private DateRange()
        {
            
        }

        public DateOnly Start { get; init; }
        public DateOnly End { get; init; }

        public int CreateDuration => End.DayNumber - Start.DayNumber;

        public static DateRange Create(DateOnly start, DateOnly end)
        {
            if (start > end)
            {
                throw new ApplicationException("Your date are invalid");
            }

            return new DateRange
            {
                Start = start,
                End = end
            };
        } 
    }
}
