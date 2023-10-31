using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklady.Export
{
    public class StatisticsCalculator
    {        
        private List<double> _weights;

        public StatisticsCalculator(List<double> lengths)
        {     
            _weights = CalculateWeights(lengths);
        }

        private List<double> CalculateWeights(List<double> lengths)
        {
            var weights = new List<double>();

            for (var i = 0; i < lengths.Count; i++)
            {
                var wi = (double) lengths[i] / lengths.Sum();
                weights.Add(wi);
            }

            return weights;
        }

        public double GetWeightedAvarage(List<double> data)
        {
            var result = 0.0;            

            for (var i = 0; i < data.Count; i++)
            {
                result += data[i] * _weights[i];
            }

            return result;
        }

        public double GetAvarage(List<double> data)
        {
            var result = 0.0;

            for (var i = 0; i < data.Count; i++)
            {
                result += data[i];
 
            }
            result = result / data.Count;
            return result;
        }


        public double GetWeightedDelta(List<double> data)
        {
            var result = 0.0;

            var squareAvg = 0.0;

            for (var i = 0; i < data.Count; i++)
            {
                squareAvg += (double) data[i] * data[i] *_weights[i];
            }

            var avg = GetWeightedAvarage(data);

            result = Math.Sqrt(squareAvg - avg * avg);

            return result;
        }

        public double GetDelta(List<double> data)
        {
            var result = 0.0;

            var squareAvg = 0.0;

            for (var i = 0; i < data.Count; i++)
            {
                squareAvg += data[i] * data[i];
            }
            squareAvg = squareAvg / data.Count;

            var avg = GetAvarage(data);

            result = Math.Sqrt(squareAvg - avg * avg);

            return result;
        }
    }
}
