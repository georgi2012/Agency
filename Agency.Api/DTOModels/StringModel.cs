namespace Agency.Api.Models
{
    public class StringModel
    {
        public string Value { get; set; }
        public StringModel(string Value)
        {
            this.Value = Value;
        }
        public static List<StringModel> CreateListOfModels(List<string> strs)
        {
            List<StringModel> stringModels = new List<StringModel>();
            foreach(var str in strs)
            {
                stringModels.Add(new StringModel(str));
            }
            return stringModels;
        }
    }
}
