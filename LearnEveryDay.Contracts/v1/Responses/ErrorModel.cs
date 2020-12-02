namespace LearnEveryDay.Contracts.V1.Responses
{
    public class ErrorModel
    {
        public ErrorModel(){}

        public ErrorModel(string fieldName, string message)
        {
            FieldName = fieldName;
            Message = message;
        }
            
        public string FieldName { get; set; }

        public string Message { get; set; }
    }
}