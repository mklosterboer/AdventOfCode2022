namespace AdventOfCode2022.Problems.Day08
{
    internal class Tree
    {
        public Tree Left { get; set; }

        public Tree Right { get; set; }

        public Tree Top { get; set; }

        public Tree Bottom { get; set; }

        public int Height { get; init; }

        public Tree(int height)
        {
            Height = height;
        }

        public bool CanBeSeen()
        {
            if (Left == null || Right == null || Top == null || Bottom == null)
            {
                return true;
            }

            if (SeenFromLeft(Height) || SeenFromRight(Height) || SeenFromBottom(Height) || SeenFromTop(Height))
            {
                return true;
            }

            return false;
        }

        public bool SeenFromLeft(int height)
        {
            if (Left == null)
            {
                return true;
            }

            if (height > Left.Height)
            {
                return Left.SeenFromLeft(height);
            }

            return false;
        }

        public bool SeenFromRight(int height)
        {
            if (Right == null)
            {
                return true;
            }

            if (height > Right.Height)
            {
                return Right.SeenFromRight(height);
            }

            return false;
        }

        public bool SeenFromTop(int height)
        {
            if (Top == null)
            {
                return true;
            }

            if (height > Top.Height)
            {
                return Top.SeenFromTop(height);
            }

            return false;
        }

        public bool SeenFromBottom(int height)
        {
            if (Bottom == null)
            {
                return true;
            }

            if (height > Bottom.Height)
            {
                return Bottom.SeenFromBottom(height);
            }

            return false;
        }

        public int GetScenicScore()
        {
            return GetLeftScore(Height, 0) * GetRightScore(Height, 0) * GetTopScore(Height, 0) * GetBottomScore(Height, 0);
        }

        public int GetLeftScore(int height, int previousScore)
        {
            if (Left == null)
            {
                return previousScore;
            }

            if (height > Left.Height)
            {
                return Left.GetLeftScore(height, previousScore + 1);
            }

            return previousScore + 1;
        }

        public int GetRightScore(int height, int previousScore)
        {
            if (Right == null)
            {
                return previousScore;
            }

            if (height > Right.Height)
            {
                return Right.GetRightScore(height, previousScore + 1);
            }

            return previousScore + 1;
        }

        public int GetTopScore(int height, int previousScore)
        {
            if (Top == null)
            {
                return previousScore;
            }

            if (height > Top.Height)
            {
                return Top.GetTopScore(height, previousScore + 1);
            }

            return previousScore + 1;
        }

        public int GetBottomScore(int height, int previousScore)
        {
            if (Bottom == null)
            {
                return previousScore;
            }

            if (height > Bottom.Height)
            {
                return Bottom.GetBottomScore(height, previousScore + 1);
            }

            return previousScore + 1;
        }
    }
}
