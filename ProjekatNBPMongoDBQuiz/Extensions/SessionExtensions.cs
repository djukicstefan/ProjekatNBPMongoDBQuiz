using Microsoft.AspNetCore.Http;
using ProjekatNBPMongoDBQuiz.Session;

namespace ProjekatNBPMongoDBQuiz.Extensions
{
    public static class SessionExtensions
    {
        public static bool IsUsernameEmpty(this ISession session)
           => string.IsNullOrEmpty(GetUsername(session));

        public static bool IsLoggedIn(this ISession session)
            => !IsUsernameEmpty(session) && GetUserId(session) != "";

        public static string GetUsername(this ISession session)
            => session.GetString(SessionKeys.Username);

        public static string GetUserId(this ISession session)
            => session.GetString(SessionKeys.UserId) ?? "";
    }
}
