using System.Linq;

namespace i15013 {
    public static class Utility {
        public static bool In<T>(this T item, params T[] list) {
            return list.Contains(item);
        }
    }
}