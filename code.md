---

코루틴 최적화

```markdown

internal static class YieldInstructionCache
{
    public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
    public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
    private static readonly Dictionary<float, WaitForSeconds> waitForSeconds = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        WaitForSeconds wfs;
        if (!waitForSeconds.TryGetValue(seconds, out wfs))
            waitForSeconds.Add(seconds, wfs = new WaitForSeconds(seconds));
        return wfs;
    }
}

```

From Client. DJ ( https://moondongjun.tistory.com/ )

---

```markdown
this 추가 사용법

public static class MonobehaviourEx
{
    public static void SetSafeActive(this MonoBehaviour target, GameObject obj, bool active = false)
    {
        if (target == null)
        {
            return;
        }

        if (obj == null)
        {
            //Debug.Log("[Safe] Active null : ", target.transform.GetFullName());
            return;
        }

        if (obj.activeSelf == active)
        {
            return;
        }

        obj.SetActive(active);
    }
}

사용예
MonoBehaviour 상속 받은 클래스에서
this.SetSafeActive(obj);

this를 사용해서 Ex함수의 target을 자동으로 넘겨줌

```

---

유니티 에디터 커스터마이징 관련 사이트

https://debuglog.tistory.com/32?category=709598

---
    
부동 소수점 비교 할때 유의점
```markdown
using System;

public class Example
{
   public static void Main()
   {
      float value1 = .3333333f;
      float value2 = 1.0f/3;
      int precision = 7;            // 소수점 자리수 제한
      
      value1 = (float) Math.Round(value1, precision);   // 소수점 수정
      value2 = (float) Math.Round(value2, precision);   // 소수점 수정
      
      Console.WriteLine("{0:R} = {1:R}: {2}", value1, value2, value1.Equals(value2));
   }
}
// The example displays the following output:
//        0.3333333 = 0.3333333: True
```
