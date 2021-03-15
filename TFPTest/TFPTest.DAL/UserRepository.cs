using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using TFPTest.Models;
using TFPTest.Models.ViewModel;

namespace TFPTest.TFPTest.DAL
{
    public class UserRepository:Controller
    {
        TFPTestDatabaseEntities _TFPTestDatabaseEntities = new TFPTestDatabaseEntities();
        internal long ExistingUser(ViewModel userData,string uploadPath)
        {
            userData.userAccount.IsActive = false;
            UserAccount user = new UserAccount();

            if (userData.userAccount.UserId == 0)
            {
                var existUser = _TFPTestDatabaseEntities.UserAccounts.Where(x => x.Email == userData.userAccount.Email).FirstOrDefault();

                if (existUser == null)
                {
                  
                    string FileName = Path.GetFileNameWithoutExtension(userData.ImageFile.FileName);
                    string FileExtension = Path.GetExtension(userData.ImageFile.FileName);

                     FileName =DateTime.Now.ToString("yyyyMMdd") + "--" + FileName.Trim() + FileExtension;
                   
                    userData.ImageFile.SaveAs(uploadPath + FileName);
                    
                    user.ImagePath = "~/Images/"+FileName;

                    user.FullName = userData.userAccount.FullName;
                    user.Email = userData.userAccount.Email;
                    user.Password = userData.userAccount.Password;
                    user.OriginType = userData.userAccount.OriginType;
                    user.ContactNumber = userData.userAccount.ContactNumber;
                    user.Nationality = userData.userAccount.Nationality;
                    user.Status = userData.userAccount.Status;
                    user.RegistrationDate = userData.userAccount.RegistrationDate;

                    _TFPTestDatabaseEntities.UserAccounts.Add(user);
                    _TFPTestDatabaseEntities.SaveChanges();

                    
                    BuildEmailTemplate(user.UserId);

                    return user.UserId;
                }
               return  0;
            }
            else
            {
                var existUser = _TFPTestDatabaseEntities.UserAccounts.Where(x => x.UserId == userData.userAccount.UserId).FirstOrDefault();

                existUser.FullName = userData.userAccount.FullName;
                existUser.Email = userData.userAccount.Email;
                existUser.Password = userData.userAccount.Password;
                existUser.OriginType = userData.userAccount.OriginType;
                existUser.ContactNumber = userData.userAccount.ContactNumber;
                existUser.Nationality = userData.userAccount.Nationality;
                existUser.Status = userData.userAccount.Status;
                existUser.RegistrationDate = DateTime.Now;

               
                _TFPTestDatabaseEntities.SaveChanges();

                return existUser.UserId;
            }
        }
        
        public void BuildEmailTemplate(long regId)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "Text" + ".cshtml");
            var regInfo = _TFPTestDatabaseEntities.UserAccounts.Where(x => x.UserId == regId).FirstOrDefault();
            var url = "http://localhost:58796/" + "Home/ConfirmAccount?userId=" + regId;
            body = body.Replace("@ViewBag.ConfirmationLink", url);
            body = body.ToString();
            BuildEmailTemplate("Your Account Is Successfully Created", body, regInfo.Email);
        }

        public static void BuildEmailTemplate(string subjectText, string bodyText, string sendTo)
        {
            string from, to, bcc, cc, subject, body;
            from = "wj48223@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectText;
            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            body = sb.ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendEmail(mail);
        }

        public static void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("wj48223@gmail.com", "01715827404");
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal bool Login(LoginData loginData, out int userId)
        {
            userId = 0;
            var currentUser = _TFPTestDatabaseEntities.UserAccounts.Where(x => x.Email == loginData.Email && x.Password==loginData.Password).FirstOrDefault();
            if (currentUser != null)
            {
                if (currentUser.IsActive == false || currentUser.IsActive==null)
                {
                    return false;
                }
                else
                {
                    userId = Convert.ToInt32(currentUser.UserId);
                    return true;
                }
            }
            return false;
        }

        internal bool LoginAfterConfirm(long userId)
        {
            var currentUser = _TFPTestDatabaseEntities.UserAccounts.Where(x => x.UserId== userId).FirstOrDefault();
            if (currentUser != null)
            {
                currentUser.IsActive = true;
                _TFPTestDatabaseEntities.SaveChanges();
                return true;
            }
            return false;
        }

        internal List<UserAccount> userDataList()
        {
            List<UserAccount> userData = _TFPTestDatabaseEntities.UserAccounts.ToList();
            return userData;
        }

        public bool DeleteAccount(int userId)
        {

            try
            {
                var userData = _TFPTestDatabaseEntities.UserAccounts.Where(x => x.UserId == userId).FirstOrDefault();
                
                _TFPTestDatabaseEntities.UserAccounts.Remove(userData);
                _TFPTestDatabaseEntities.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool EditAccount(UserAccount userData)
        {

            try
            {
                var user = _TFPTestDatabaseEntities.UserAccounts.Where(x => x.UserId == userData.UserId).FirstOrDefault();

                user.FullName = userData.FullName;
                user.Email = userData.Email;
                user.Password = userData.Password;
                user.OriginType = userData.OriginType;
                user.ContactNumber = userData.ContactNumber;
                user.Nationality = userData.Nationality;
                user.Status = userData.Status;
                user.RegistrationDate = userData.RegistrationDate;

                _TFPTestDatabaseEntities.SaveChanges();
                return true;
            }
            catch { return false; }
        }
    }
}