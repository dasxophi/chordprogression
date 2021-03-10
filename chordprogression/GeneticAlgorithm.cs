using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chordprogression
{
    class GeneticAlgorithm<T>
    {
        public List<DNA<T>> Population { get; private set; } //DNAのリスト
        public int Generation { get; private set; } //世代番号蓄積用
        public float BestFitness { get; private set; }　//先代の最もfitness値
        public T[] BestGenes { get; private set; }　//先代の最もfitnessが高いDNAの遺伝子

        public float MutationRate;

        private Random random;
        private float fitnessSum; //Populationの全てのDNAのfitnessの合計

        public GeneticAlgorithm(int populationSize, int DNAsize, Random random, Func<T> getRandomGene, Func<int, float> fitnessFunction, float mutationRate = 0.01f)
        {
            Generation = 1; // 世代初期化
            MutationRate = mutationRate; // mutation率設定
            Population = new List<DNA<T>>();　//Population初期化
            this.random = random;

            for(int i = 0; i < populationSize; i++)
            {
                //PopulationにDNAを追加していく
                Population.Add(new DNA<T>(DNAsize, random, getRandomGene, fitnessFunction, shouldInitGenes: true));

            }
        }

        public void NewGeneration()
        {
            if(Population.Count <= 0)
            {
                return; //Populationが0以下なら終了
            }
            CalculateFitness();

            List<DNA<T>> newPopulation = new List<DNA<T>>(); //先代から生成された新しいDNAの集合

            for(int i = 0; i < Population.Count; i++) //先代Populationの数だけchildが生成されるので
            {
                DNA<T> parent1 = ChooseParent();
                DNA<T> parent2 = ChooseParent();

                DNA<T> child = parent1.Crossover(parent2);　//parent1とparent2とのcrossoverによってchild生成

                child.Mutate(MutationRate); //MutationRateの確率でmutationが起こる

                newPopulation.Add(child); //新しいPopulationに生成されたchildを追加
            }

            Population = newPopulation;　//生成が全部終わったら新しい世代を現世代とする

            Generation++; //世代数を1増やす
        }
        public void CalculateFitness()
        {
            fitnessSum = 0;
            DNA<T> best = Population[0];

            for (int i = 0; i < Population.Count; i++)
            {
                fitnessSum += Population[i].CalculateFitness(i);　//Populationのi番目DNAとのfitnessを計算

                if(Population[i].Fitness > best.Fitness)　//以前のbestのfitnessよりi番目のDNAのfitnessが高かったら
                {
                    best = Population[i];   //そのi番目のDNAをbestとする
                }
            }

            BestFitness = best.Fitness; //bestのfitnessをBestfitnessとして保存しておく
            best.Genes.CopyTo(BestGenes, 0); //bestの遺伝子をBestGenesに保存しておく(0番目の遺伝子から全部)

        }

        private DNA<T> ChooseParent() //parentを選択
        {
            double randomNumber = random.NextDouble() * fitnessSum;

            for(int i = 0; i < Population.Count; i++)
            {
                if(randomNumber < Population[i].Fitness) // Populationのi番目のDNAのfitnessがrandomNumberより大きかったら
                {
                    return Population[i]; //そのi番目のDNAをparentとして返す
                }

                randomNumber -= Population[i].Fitness;
            }

            return null;
        }

    }


}
