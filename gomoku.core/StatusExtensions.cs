namespace gomoku.core
{
    public static class StatusExtensions
    {
        public static Status Inverse(this Status status)
        {
            return status switch
            {
                Status.White => Status.Black,
                Status.Black => Status.White,
                Status.Free => Status.Free,
                Status.Border => Status.Border,
                _ => Status.Free,
            };
        }

        public static string PrintColor(this Status color)
        {
            return $" {color.ToChar()} ";
        }

        public static char ToChar(this Status status)
        {
            return status switch
            {
                Status.Black => 'X',
                Status.White => '0',
                Status.Free => '-',
                Status.Border => '+',
                _ => '-',
            };
        }

    }
}
