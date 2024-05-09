namespace CursVN.Core
{
    public class ModelWrapper<TModel>
    {
        public TModel Model { get; private set; }
        public string ErrorMessage { get; private set; }
        public bool IsValid { get; private set; }
        
        public ModelWrapper(TModel model, string error, bool valid)
        {
            Model = model;
            ErrorMessage = error;
            IsValid = valid;
        }
    }
}
