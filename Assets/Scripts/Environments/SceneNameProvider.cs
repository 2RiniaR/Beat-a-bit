using System.Linq;
using Mono.Collections.Generic;

namespace RineaR.BeatABit.Environments
{
    public static class SceneNameProvider
    {
        private static readonly ReadOnlyCollection<SceneMapping> Mappings = new(new[]
        {
            new SceneMapping { Type = SceneType.Common, Name = "Common" },
            new SceneMapping { Type = SceneType.Initialize, Name = "Initialize" },
            new SceneMapping { Type = SceneType.StagePlay, Name = "Stage Play" },
            new SceneMapping { Type = SceneType.StageSelect, Name = "Stage Select" },
            new SceneMapping { Type = SceneType.Title, Name = "Title" },
        });

        public static string GetName(SceneType type)
        {
            return Mappings.First(match => match.Type == type).Name;
        }

        public static SceneType? GetType(string name)
        {
            return Mappings.FirstOrDefault(match => match.Name == name)?.Type;
        }

        private class SceneMapping
        {
            public SceneType Type { get; set; }
            public string Name { get; set; }
        }
    }
}