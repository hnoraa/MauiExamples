namespace CarListApp.Maui.Controls;

public partial class FlyoutHeader : StackLayout
{
	public FlyoutHeader()
	{
		InitializeComponent();
		SetValues();
	}

    private void SetValues()
    {
        if(App.userInfo != null)
		{
			lblUserName.Text = App.userInfo.UserName;
			lblRole.Text = App.userInfo.Role;
		}
    }
}