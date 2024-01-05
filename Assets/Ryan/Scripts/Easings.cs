using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Easings {

    //Some constant variables to make writing out each ease alot easier
    public const float n1 = 7.5625f;
    public const float d1 = 2.75f;
    public const float c1 = 1.70158f;
    public const float c2 = c1 * 1.525f;
    public const float c3 = c1 + 1;
    public const float c4 = (2f * Mathf.PI) / 3f;
    public const float c5 = (2f * Mathf.PI) / 4.5f;
    
    //Linear ease
    public static float Linear(float t) {
        return t;
    }

    //Quadratic eases
    public class Quadratic {
        public static float In(float t) {
            return t * t;
        }
        public static float Out(float t) {
            return t * (2f - t);
        }

        public static float InOut(float t) {
            if ((t *= 2f) < 1f) return 0.5f * t * t;
            return -0.5f * ((t -= 1f) * (t - 2f) - 1f);
        }

        public static float Bezier(float t) {
            return 2 * t * (1 - t) + t * t;
        }
    }

    //Trigonometric eases
    public class Trigonometric {
        public static float SIn(float t) {
            return 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
        }


        public static float SOut(float t) {
            return 1f - Mathf.Sin(t * Mathf.PI * 0.5f);
        }

        public static float SinInOut(float t) {
            if ((t *= 2f) < 1f) return 0.5f * (1f - Mathf.Cos(t * Mathf.PI * 0.5f));
            return -0.5f * Mathf.Sin(t * Mathf.PI * 0.5f);
        }
    }

    //Elastic eases
    public class Elastic {
        public static float EaseInElastic(float t) {
            if (t == 0) {
                return 0;
            } 
            else if (t == 1) {
                return 1;
            } 
            else {
                return -Mathf.Pow(2, 10 * t - 10) * Mathf.Sin((t * 10f - 10.75f) * c4);
            }
        }

        public static float EaseOutElastic(float t) {
            if (t == 0) {
                return 0;
            } else if (t == 1) {
                return 1;
            } else {
                return Mathf.Pow(2, -10 * t) * Mathf.Sin((t * 10f - 0.75f) * c4) + 1;
            }
        }

        public static float easeInOutElastic(float t) {
            if (t == 0) {
                return 0;
            } else if (t == 1) {
                return 1;
            } else if (t < 0.5) {
                return -(Mathf.Pow(2f, 20f * t - 10f) * Mathf.Sin((20f * t - 11.125f) * c5)) / 2;
            }
            return (Mathf.Pow(2, -20 * t + 10) * Mathf.Sin((20 * t - 11.125f) * c5)) / 2 + 1;
        }
    }

    //Bounce eases
    public class Bounce {
        public static float easeInBounce(float t) {
            return 1 - easeOutBounce(1 - t);
        }

        public static float easeOutBounce(float t) {
            if (t < 1 / d1) {
                return n1 * t * t;
            } else if (t < 2 / d1) {
                return n1 * (t -= 1.5f / d1) * t + 0.75f;
            } else if (t < 2.5f / d1) {
                return n1 * (t -= 2.25f / d1) * t + 0.9375f;
            } else {
                return n1 * (t -= 2.625f / d1) * t + 0.984375f;
            }
        }
        public static float easeInOutBounce(float t) {
            if (t < 0.5) {
                return (1 - easeOutBounce(1 - 2 * t)) / 2;
            } else {
                return (1 + easeOutBounce(2 * t - 1)) / 2;
            }
        }
    }

    //Cubic eases
    public class Cubic {
        public static float easeInCubic(float t) {
            return t * t * t;
        }
        public static float easeOutCubic(float t) {
            return 1 - Mathf.Pow(1 - t, 3);
        }
        public static float easeInOutCubic(float t) {
            return t < 0.5 ? 4 * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
        }
    }

    //Quarternary eases
    public class Quart {
        public static float easeInQuart(float t) {
            return t * t * t * t;
        }
        public static float easeOutQuart(float t) {
            return 1 - Mathf.Pow(1 - t, 4);
        }
        public static float easeInOutQuart(float t) {
            return t < 0.5f ? 8 * t * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 4) / 2;
        }
    }

    //Quint eases
    public class Quint {
        public static float easeInQuint(float t) {
            return t * t * t * t * t;
        }
        public static float easeOutQuint(float t) {
            return 1 - Mathf.Pow(1 - t, 5);
        }
        public static float easeInOutQuint(float t) {
            return t < 0.5f ? 16 * t * t * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 5) / 2;
        }
    }

    //Exponential eases
    public class Expo {
        public static float easeInExpo(float t) {
            if (t == 0) {
                return 0;
            } else {
                return Mathf.Pow(2, 10 * t - 10);
            }
        }
        public static float easeOutExpo(float t) {
            if (t == 1) {
                return 1;
            } else {
                return 1 - Mathf.Pow(2, -10 * t);
            }
        }
        public static float easeInOutExpo(float t) {
            if (t == 0) {
                return 0;
            } else if (t == 1) {
                return 1;
            } else if (t < 0.5) {
                return Mathf.Pow(2, 20 * t - 10) / 2;
            } else {
                return (2 - Mathf.Pow(2, -20 * t + 10)) / 2;
            }
        }
    }

    //Circ eases
    public class Circ {
        public static float EaseInCirc(float t) {
            return 1 - Mathf.Sqrt(1 - Mathf.Pow(t, 2));
        }
        public static float EaseOutCirc(float t) {
            return Mathf.Sqrt(1 - Mathf.Pow(t - 1, 2));
        }
        public static float EaseInOutCirc(float t) {
            if (t < 0.5f) {
                return (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * t, 2))) / 2;
            } 
            else {
                return (Mathf.Sqrt(1 - Mathf.Pow(-2 * t + 2, 2)) + 1) / 2;
            }
        }
    }

    //Back eases
    public class Back {
        public static float easeInBack(float t) {
            return c3 * t * t * t - c1 * t * t;
        }
        public static float easeOutBack(float t) {
            return 1 + c3 * Mathf.Pow(t - 1, 3) + c1 * Mathf.Pow(t - 1, 2);
        }
        public static float easeInOutBack(float t) {
            if (t < 0.5f) {
                return (Mathf.Pow(2 * t, 2) * ((c2 + 1) * 2 * t - c2)) / 2;
            } 
            else {
                return (Mathf.Pow(2 * t - 2, 2) * ((c2 + 1) * (t * 2 - 2) + c2) + 2) / 2;
            }
        }
    }
}



