using System.Text;

namespace Market.UseCases.Exceptions
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public List<string> Messages { get; set; } = new List<string>();

        public string ConvertToOneMessage()
        {
            var strBuilder = new StringBuilder();

            foreach (var item in Messages)
            {
                strBuilder.Append(item).Append(".\n");
            }

            return strBuilder.ToString();
        }
    }
}
