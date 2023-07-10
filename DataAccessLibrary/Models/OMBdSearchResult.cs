using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;


namespace DataAccessLibrary.Models
{
	public class OMBdSearchResult
	{
        [JsonPropertyName("Title")]
        public string Title { get; set; }

        [JsonPropertyName("Released")]
        public string Released { get; set; }

        [JsonPropertyName("Runtime")]
        public string Runtime { get; set; }

        [JsonPropertyName("Ratings")]
        public List<Rating> Ratings { get; set; }

        [JsonPropertyName("Actors")]
        public string Actors { get; set; }

        [JsonPropertyName("Response")]
        public string? Response { get; set; }

        public string ErrorMessage { get; set; }

        public OMBdSearchResult() { }

        public OMBdSearchResult(string Title, string Released, string Runtime, List<Rating> Ratings, string Actors, string? Response)
        {
            this.Title = Title;
            this.Released = Released;
            this.Runtime = Runtime;
            this.Ratings = Ratings;
            this.Actors = Actors;
            this.Response = Response;
        }
    }

    public partial class Rating
    {
        [JsonPropertyName("Source")]
        public string Source { get; set; }

        [JsonPropertyName("Value")]
        public string Value { get; set; }

        public int GetOMBdRating()
        {
            if (Source == "Internet Movie Database")
            {
                float rating = float.Parse(Value.Split('/')[0]);
                int imdbRating = (int)(rating * 10);
                return imdbRating;
            }
            else
            {
                return 0;
            }
        }
    }


    internal class ParseStringConverter : JsonConverter<long>
    {
        public override bool CanConvert(Type t) => t == typeof(long);

        public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.ToString(), options);
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}

