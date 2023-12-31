﻿namespace RabbitResurrection
{
    public class Define
    {
        public enum Scene
        {
            Unknown,
            OpenScene,
            TitleScene,
            MergeScene_Environment,
            EndScene,
        }

        public enum MouseEvent
        {
            Press,
            Click,
        }

        public enum Sound
        {
            BGM,
            Effect,
            MaxCount,
        }

        public enum UIEvent
        {
            Click,
            Drag,
        }

        public enum Town
        {
            None,
            Initial,
            I,
            E,
            L,
            N,
            O,
            A,
            S,
            X
        }
    }
}