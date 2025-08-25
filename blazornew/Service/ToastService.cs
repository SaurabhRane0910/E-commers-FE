namespace blazornew.Service
{
    public class ToastService
    {
        public event Func<string, string, Task> OnShow;

        
        public void ShowSuccess(string message) => Fire("success", message);
        public void ShowError(string message) => Fire("danger", message);
        public void ShowInfo(string message) => Fire("info", message);
        public void ShowWarning(string message) => Fire("warning", message);

        private void Fire(string type, string message)
        {
            var handler = OnShow;
            if (handler != null)
            {
                
                _ = handler.Invoke(type, message);
            }
        }
    }
}