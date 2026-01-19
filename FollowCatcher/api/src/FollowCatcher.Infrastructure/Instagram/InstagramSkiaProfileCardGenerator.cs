using FollowCatcher.Domain.Instagram;
using SkiaSharp;
using Microsoft.Extensions.Logging;

namespace FollowCatcher.Infrastructure.Instagram;

public class InstagramSkiaProfileCardGenerator(ILogger<InstagramSkiaProfileCardGenerator> logger) : IInstagramProfileCardGenerator
{
    public Task<byte[]> GenerateCardAsync(InstagramProfileInfo profile, byte[] avatarBytes)
    {
        try 
        {
            // Dimensions similar to screenshot
            int width = 800;
            int height = 400;

            using var surface = SKSurface.Create(new SKImageInfo(width, height));
            var canvas = surface.Canvas;

            // Background Color (Dark Theme)
            canvas.Clear(new SKColor(20, 20, 20)); // Dark background

            // Avatar Dimensions
            int avatarSize = 180;
            int avatarX = 120;
            int avatarY = (height - avatarSize) / 2;

            // Load Avatar
            using var avatarBitmap = SKBitmap.Decode(avatarBytes);

            using (var paint = new SKPaint { IsAntialias = true, FilterQuality = SKFilterQuality.High })
            {
                // Circular Clip
                canvas.Save();
                var path = new SKPath();
                path.AddCircle(avatarX + avatarSize / 2, avatarY + avatarSize / 2, avatarSize / 2);
                canvas.ClipPath(path);
                
                // Draw Scaled Bitmap
                var rect = new SKRect(avatarX, avatarY, avatarX + avatarSize, avatarY + avatarSize);
                canvas.DrawBitmap(avatarBitmap, rect, paint);
                canvas.Restore();
            }

            // Draw Text
            var textX = avatarX + avatarSize + 60;
            var textStartY = avatarY + 40;

            // Username
            using (var usernamePaint = new SKPaint
            {
                Color = SKColors.White,
                TextSize = 36, // Slightly larger
                IsAntialias = true,
                Typeface = SKTypeface.FromFamilyName("Segoe UI", SKFontStyle.Bold) // Better font
            })
            {
                canvas.DrawText(profile.Username, textX, textStartY, usernamePaint);
            }

            // Stats
            var statsY = textStartY + 60;
              
            // Draw Stats
            float currentX = textX;
            currentX = DrawStat(canvas, currentX, statsY, profile.PostCount.ToString(), " gönderi");
            currentX += 40; // padding
            currentX = DrawStat(canvas, currentX, statsY, profile.FollowerCount.ToString(), " takipçi");
            currentX += 40; // padding
            currentX = DrawStat(canvas, currentX, statsY, profile.FollowingCount.ToString(), " takip");

            // Full Name
            float nameY = statsY + 60;
            if (!string.IsNullOrWhiteSpace(profile.FullName))
            {
                using var namePaint = new SKPaint
                {
                    Color = new SKColor(220, 220, 220), // Slightly grey
                    TextSize = 24,
                    IsAntialias = true,
                    Typeface = SKTypeface.FromFamilyName("Segoe UI", SKFontStyle.Bold)
                };
                canvas.DrawText(profile.FullName, textX, nameY, namePaint);
            }

            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            return Task.FromResult(data.ToArray());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to generate profile card");
            throw;
        }
    }

    private float DrawStat(SKCanvas canvas, float x, float y, string number, string label)
    {
         using var boldPaint = new SKPaint { Color = SKColors.White, TextSize = 24, IsAntialias = true, Typeface = SKTypeface.FromFamilyName("Segoe UI", SKFontStyle.Bold) };
         using var normalPaint = new SKPaint { Color = new SKColor(180, 180, 180), TextSize = 22, IsAntialias = true, Typeface = SKTypeface.FromFamilyName("Segoe UI", SKFontStyle.Normal) };

         canvas.DrawText(number, x, y, boldPaint);
         float numberWidth = boldPaint.MeasureText(number);
         
         float labelX = x + numberWidth + 5;
         canvas.DrawText(label, labelX, y, normalPaint);
         float labelWidth = normalPaint.MeasureText(label);

         return labelX + labelWidth;
    }
}
