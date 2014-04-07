namespace ProgressTen.Infrastructure.ViewModels
{
	public class MenuTab : ViewModelBase
    {
        private MenuTab(string text, string action, string controller, string area)
        {
            this.Text = text;
            this.Action = action;
            this.Controller = controller;
            this.Area = area;
        }

        public string Action { get; private set; }

        public string Controller { get; private set; }

        public string Text { get; private set; }

        public string Area { get; private set; }

        public static MenuTab Create(string text, string action, string controller, string area)
        {
            return new MenuTab(text, action, controller, area);
        }
    }
}