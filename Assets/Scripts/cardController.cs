using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardController : MonoBehaviour {

    public GameObject cardFront;
    public GameObject cardBack;
    public float flipDuration;

    private bool flipped = false;
    private bool isFlipping = false;

	// Use this for initialization
	void Start () {
        flipCard();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("clicked");
            flipCard();
        }
	}

    void flipCard()
    {
        if (!isFlipping)
        {
            if (!flipped)
            {
                StartCoroutine(flip(cardFront, 180f, false));
                StartCoroutine(flip(cardBack, 0f, true));

            }
            else
            {
                StartCoroutine(flip(cardFront, 0f, true));
                StartCoroutine(flip(cardBack, 180f, false));
            }
            flipped = !flipped;
        }
    }

    IEnumerator flip(GameObject target, float degrees, bool change) {
        bool hasChangedLayer = false;
        float elapsedTime = 0f;
        isFlipping = true;

        Quaternion startingRotation = target.transform.rotation; // have a startingRotation as well
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0.0f, degrees, 0.0f));
        while (elapsedTime < flipDuration)
        {
            elapsedTime += Time.deltaTime;
            target.transform.rotation = Quaternion.Slerp(startingRotation, targetRotation, (elapsedTime / flipDuration));
            if (elapsedTime / flipDuration > 0.5 && !hasChangedLayer && change)
            {
                target.transform.SetAsLastSibling();
                hasChangedLayer = true;
            }
            yield return new WaitForEndOfFrame();
        }
        isFlipping = false;
    }
}



