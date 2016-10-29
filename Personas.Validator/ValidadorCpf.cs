
namespace Personas.Validator
{
    public static class ValidadorCpf
    {
        public static bool Validar(string item)
        {
            int[] Multiplier1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] Multiplier2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            long cpfValidNumber;

            if (string.IsNullOrEmpty(item))
                return false;

            item = item.Trim().Replace(".", "").Replace("-", "");

            if (!long.TryParse(item, out cpfValidNumber))
                return false;

            item = cpfValidNumber.ToString("D11");

            switch (item)
            {
                case "00000000000":
                case "11111111111":
                case "2222222222":
                case "33333333333":
                case "44444444444":
                case "55555555555":
                case "66666666666":
                case "77777777777":
                case "88888888888":
                case "99999999999":
                    return false;
            }

            var tempCpf = item.Substring(0, 9);

            var sum = CalculateSum(tempCpf, Multiplier1);
            var rest = CalculateRest(sum);

            var digit = rest.ToString();

            tempCpf = tempCpf + digit;

            sum = CalculateSum(tempCpf, Multiplier2);
            rest = CalculateRest(sum);

            digit = digit + rest.ToString();

            return item.EndsWith(digit);
        }

        private static int CalculateRest(int sum)
        {
            var rest = sum % 11;

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            return rest;
        }

        private static int CalculateSum(string cpfWithoutDigit, int[] multiplier)
        {
            var sum = 0;

            for (var i = 0; i < multiplier.Length; i++)
                sum += int.Parse(cpfWithoutDigit[i].ToString()) * multiplier[i];

            return sum;
        }
    }
}
