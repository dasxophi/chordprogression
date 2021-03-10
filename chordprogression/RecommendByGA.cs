using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chordprogression
{
    class RecommendByGA
    {
        //基本設定
        int populationSize;
        float mutationRate = 0.01f;
        public List<String> FirstPopulation { get; private set; } //データベースのコード進行をn-gramで分割してここに保存
        private System.Random random;

        private GeneticAlgorithm<string> GA;
    }
}
