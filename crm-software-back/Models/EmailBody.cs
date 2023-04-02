namespace crm_software_back.Models
{
    public static class EmailBody
    {
        public static string EmailStringBody1(string email, string emailToken, string firstName)
        {
            return $@"<html>
                <head>
                </head>
                <body style=""margin:0;padding:0;font-family:Arial, Helvectica, sans-serif;"">
                    <div style=""height: auto; background: linear-gradient(to right, #a8dadc, #f1faee) no-repeat; width:400px; padding:30px"">
                        <div>
                            <div>
                                <h1>
                                    Reset your password
                                </h1>
                                <hr>
                                <p>
                                    Dear {firstName},
                                    <br>
                                    <br>
                                    We’ve received a password reset request. Click the button below to securely reset your password. 
                                    <br>
                                    <br>
                                    If you did not request a password reset, please ignore this email or contact our support team.
                                    <br>
                                    <br>
                                    <a href=""http://localhost:4200/reset?email={email}&code={emailToken}"" target=""_blank"" style=""background-color:#457b9d;padding:10px; border:none;color:#fff;border-radius:4px;display:block;margin:0 auto;width:50%;text-align:center;text-decoration:none"">
                                        Reset Password
                                    </a>
                                    <br>
                                    <br>
                                    Thank you for choosing our CRM software to manage your business needs.
                                    <br>
                                    <br>
                                    Kind regards,
                                    <br>
                                    <br>
                                    Admin
                                    <br>
                                    CRM
                                </p>
                            </div>
                        </div>
                    </div>
                </body>
            </html>";
        }

        public static string NewUserEmail(string username, string password, string firstName)
        {
            return $@"<html>
                <head>
                </head>
                <body style=""margin:0;padding:0;font-family:Arial, Helvectica, sans-serif;"">
                    <div style=""height: auto; background: linear-gradient(to right, #a8dadc, #f1faee) no-repeat; width:400px; padding:30px"">
                        <div>
                            <div>
                                <h1>
                                    Welcome to the CRM
                                </h1>
                                <hr>
                                <p> 
                                    Dear {firstName},
                                    <br>
                                    <br>
                                    We are pleased to inform you that your account has been successfully created in our CRM software. Your login details are as follows:
                                    <br>
                                    <br>
                                    Username: {username}
                                    <br>
                                    Password: {password}
                                    <br>
                                    <br>
                                    Please ensure that you keep this information safe and secure. If you have any questions or concerns about your account, please don't hesitate to contact our support team.
                                    <br>
                                    <br>
                                    <a href=""http://localhost:3000"" target=""_blank"" style=""background-color:#457b9d;padding:10px; border:none;color:#fff;border-radius:4px;display:block;margin:0 auto;width:50%;text-align:center;text-decoration:none"">
                                        Login
                                    </a>
                                    <br>
                                    <br>
                                    Thank you for choosing our CRM software to manage your business needs.
                                    <br>
                                    <br>
                                    Kind regards,
                                    <br>
                                    <br>
                                    Admin
                                    <br>
                                    CRM
                                </p>
                            </div>
                        </div>
                    </div>
                </body>
            </html>";
        }
    }
}

