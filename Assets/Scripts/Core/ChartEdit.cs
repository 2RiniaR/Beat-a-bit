using System.Collections.Generic;
using JetBrains.Annotations;
using RineaR.BeatABit.Helpers;

namespace RineaR.BeatABit.Core
{
    public class ChartEdit
    {
        private List<Badge> _badges;

        public ChartEdit(Chart chart)
        {
            Chart = chart;
            ResetEditing();
        }

        public Chart Chart { get; }

        public void ResetEditing()
        {
            _badges = new List<Badge>();
            foreach (var beat in Chart.beats)
            {
                _badges.Add(beat.locked);
            }
        }

        public void Assign(int beatNumber, [CanBeNull] Badge badge)
        {
            var beat = Chart.BeatOf(beatNumber);
            if (beat == null || beat.locked) return;

            _badges[beatNumber - 1] = badge;
        }

        public Badge BadgeOf(int beatNumber)
        {
            return _badges.TryAccess(beatNumber - 1);
        }

        public ChartArrange PublishArrange()
        {
            return new ChartArrange { badges = new List<Badge>(_badges) };
        }
    }
}