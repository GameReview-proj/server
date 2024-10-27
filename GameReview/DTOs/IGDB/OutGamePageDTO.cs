namespace GameReview.DTOs.IGDB;
public record OutGamePageDTO(
        IEnumerable<ExternalApiGame> Games,
        int? Total
    )
{

}