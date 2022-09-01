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
this를 사용해서 Ex함수의 target을 자동으로 넘겨주는 것 같다.

```

---
