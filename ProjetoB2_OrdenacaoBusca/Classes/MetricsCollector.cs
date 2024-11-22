using System;

namespace ProjetoB2_OrdenacaoBusca
{
    public class MetricsCollector
    {
        private int ComparacoesBubble;
        private int ComparacoesSelection;
        private int ComparacoesInsertion;
        private int ComparacoesQuick;
        private int ComparacoesMerge;

        // Bubble Sort
        public void incComparacoesBubble()
        {
            this.ComparacoesBubble++;
        }

        public int getComparacoesBubble()
        {
            return this.ComparacoesBubble;
        }

        // Selection Sort
        public void incComparacoesSelection()
        {
            this.ComparacoesSelection++;
        }

        public int getComparacoesSelection()
        {
            return this.ComparacoesSelection;
        }

        // Insertion Sort
        public void incComparacoesInsertion()
        {
            this.ComparacoesInsertion++;
        }

        public int getComparacoesInsertion()
        {
            return this.ComparacoesInsertion;
        }

        // Quick Sort
        public void incComparacoesQuick()
        {
            this.ComparacoesQuick++;
        }

        public int getComparacoesQuick()
        {
            return this.ComparacoesQuick;
        }

        // Merge Sort
        public void incComparacoesMerge()
        {
            this.ComparacoesMerge++;
        }

        public int getComparacoesMerge()
        {
            return this.ComparacoesMerge;
        }
    }
}
