using System;
using System.Windows;
using System.Windows.Navigation;
using IdentityModel.Client;

namespace Thinktecture.Samples
{
  public partial class LoginWebView : Window
  {
    private Uri _callbackUri;

    public LoginWebView()
        {
            InitializeComponent();
            webView.Navigating += webView_Navigating;

            Closing += LoginWebView_Closing;
        }

    public event EventHandler<AuthorizeResponse> Done;

    public AuthorizeResponse AuthorizeResponse { get; set; }

    public void Start(Uri startUri, Uri callbackUri)
        {
            _callbackUri = callbackUri;
            webView.Navigate(startUri);
        }

    private void LoginWebView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      e.Cancel = true;
      this.Visibility = Visibility.Hidden;
    }

    private void webView_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.Uri.ToString().StartsWith(_callbackUri.AbsoluteUri))
            {
                AuthorizeResponse = new AuthorizeResponse(e.Uri.AbsoluteUri);

                e.Cancel = true;
                this.Visibility = System.Windows.Visibility.Hidden;

                if (Done != null)
                {
                    Done.Invoke(this, AuthorizeResponse);
                }
            }
        }
  }
}