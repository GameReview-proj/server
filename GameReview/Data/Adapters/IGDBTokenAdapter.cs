using GameReview.Data.JsonObjects;

namespace GameReview.Data.Adapters;

public static class IGDBTokenAdapter
{
    public static void UpdateToken(IGDBToken originalToken, IGDBToken newToken)
    {
        originalToken.AccessToken = newToken.AccessToken;
        originalToken.ExpiresIn = newToken.ExpiresIn;
    }
}