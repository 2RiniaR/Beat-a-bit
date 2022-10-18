using System.Collections.Generic;
using JetBrains.Annotations;
using RineaR.BeatABit.Helpers;

namespace RineaR.BeatABit.Core
{
    public class ChartEdit
    {
        private List<Badge> _bits;

        public ChartEdit(Chart chart)
        {
            Chart = chart;
            ResetEditing();
        }

        public Chart Chart { get; }

        public void ResetEditing()
        {
            _bits = new List<Badge>();
            foreach (var beat in Chart.beats)
            {
                _bits.Add(beat.locked);
            }
        }

        public void AssignBit(int beatNumber, [CanBeNull] Badge badge)
        {
            var beat = Chart.BeatOf(beatNumber);
            if (beat == null || beat.locked) return;

            _bits[beatNumber - 1] = badge;
        }

        public Badge BitOf(int beatNumber)
        {
            return _bits.TryAccess(beatNumber - 1);
        }

        public ChartArrange PublishArrange()
        {
            return new ChartArrange { bits = new List<Badge>(_bits) };
        }
    }
}