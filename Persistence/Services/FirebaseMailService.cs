using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;

namespace Persistence.Services
{
    public class FirebaseMailService
    {
        private readonly FirestoreDb _firestoreDb;

        public FirebaseMailService(string firebaseAdminSdkJsonPath)
        {
            var credentials = GoogleCredential.FromFile(firebaseAdminSdkJsonPath);
            FirebaseApp.Create(new AppOptions
            {
                Credential = credentials
            });
            _firestoreDb = FirestoreDb.Create("ums-31836"); 
        }

        public async Task SendMail(string recipient, string subject, string body)
        {
            var message = new Message
            {
                Notification = new Notification
                {
                    Title = subject,
                    Body = body,
                },
                Token = recipient,
            };
            var messaging = FirebaseMessaging.DefaultInstance;
            await messaging.SendAsync(message);
        }
    }
}