using UnityEngine;

namespace Nuker.Tools {
    public interface ICutter {
        public int PieceCount {get;set;}

        public void Cut(int pieceCount);
        public Vector2 GetCut(int cutIndex);
        public int PieceSelection(Vector2 position);
    }

    public class CircleCutter: ICutter {
        public int PieceCount {get;set;}

        public void Cut(int pieceCount) {
            PieceCount = pieceCount;
        }

        public Vector2 GetCut(int cutIndex)
        {
            float angle = 360 * Mathf.Deg2Rad * cutIndex / PieceCount;
            Vector2 direction = new(Mathf.Sin(angle), Mathf.Cos(angle));
            return direction;
        }

        // here position is direction
        public int PieceSelection(Vector2 position)
        {
            float angle = Vector2.SignedAngle(Vector2.up, position);
            if(angle < 0) angle = 360 + angle;
            angle = 360 - angle;
            float anglePerSlice = 360 / PieceCount;
            if(angle < 360 && angle > 360 - (anglePerSlice/2)) {
                return 0;
            } else {
                return (int)Mathf.Floor(angle / anglePerSlice) + 1;
            }
        }
    }

    public class HorizontalCutter : ICutter
    {
        public int PieceCount {get;set;}

        public void Cut(int pieceCount) {
            PieceCount = pieceCount;
        }
        // returning relative position of the cut
        public Vector2 GetCut(int cutIndex)
        {
            return Vector2.up * (float)cutIndex / (float)PieceCount;
        }
        // here position is relative position
        public int PieceSelection(Vector2 position)
        {
            return position.x < 0 ? 0 : Mathf.FloorToInt(position.x * PieceCount);
        }
    }

    public class VerticalCutter : ICutter
    {
        public int PieceCount {get;set;}

        public void Cut(int pieceCount) {
            PieceCount = pieceCount;
        }
        // returning relative position of the cut
        public Vector2 GetCut(int cutIndex)
        {
            return Vector2.right * (float)cutIndex / (float)PieceCount;
        }
        // here position is relative position
        public int PieceSelection(Vector2 position)
        {
            return position.x < 0 ? 0 : Mathf.FloorToInt(position.y * PieceCount);
        }
    }
}