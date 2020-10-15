using Amazon.CognitoIdentityProvider.Model;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class SignUp : ContentPage
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string p = await CognitoSignUp(UserNameTextBox.Text, PasswordTextBox.Text, EmailTextBox.Text);
            if (!p.StartsWith("Error"))
            {
                await DisplayAlert("Signed Up", "Successful. " + p, "Ok");
            }
            else
            {
                await DisplayAlert("Sign Up Error", p, "Ok");
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            string v = await CognitoConfirmMailCode(UserNameTextBox.Text, ConfirmationTextBox.Text);
            if (!v.StartsWith("Error"))
                await DisplayAlert("Confirmed", "Email confirmed. " + v, "Ok");
            else
                await DisplayAlert("Error", "Code Error. " + v, "Ok");
        }

        private async Task<string> CognitoConfirmMailCode(string userName, string confirmationCode)
        {
            try
            {
                ConfirmSignUpRequest confirmRequest = new ConfirmSignUpRequest()
                {
                    Username = userName,
                    ClientId = AWS.ClientID,
                    ConfirmationCode = confirmationCode
                };

                ConfirmSignUpResponse confirmResult = await App.provider.ConfirmSignUpAsync(confirmRequest);
                return confirmResult.ResponseMetadata.RequestId;
            }
            catch (Exception ex)
            {
                return "Error " + ex.Message;
            }
        }

        public async Task<string> CognitoSignUp(string userName, string password, string email)
        {
            try
            {
                SignUpRequest signUpRequest = new SignUpRequest()
                {
                    ClientId = AWS.ClientID,
                    Password = password,
                    Username = userName
                };
                AttributeType emailAttribute = new AttributeType()
                {
                    Name = "email",
                    Value = email
                };
                signUpRequest.UserAttributes.Add(emailAttribute);

                SignUpResponse signUpResult = await App.provider.SignUpAsync(signUpRequest);

                return signUpResult.UserSub;
            }
            catch (Exception e)
            {
                return "Error " + e.Message;
            }
        }
    }
}