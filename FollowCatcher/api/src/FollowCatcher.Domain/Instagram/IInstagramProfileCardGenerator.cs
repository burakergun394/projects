namespace FollowCatcher.Domain.Instagram;

public interface IInstagramProfileCardGenerator
{
    Task<byte[]> GenerateCardAsync(InstagramProfileInfo profile, byte[] avatarBytes);
}
