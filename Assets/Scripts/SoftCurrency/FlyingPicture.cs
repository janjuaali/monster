using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingPicture : MonoBehaviour
{
    [SerializeField] private Image _image;

    private float _time;

    public void Init(Vector3 destination, Sprite sprite, float size, WalletView walletView, float amount, float time)
    {
        _time = time;
        SetSprite(sprite);
        StartCoroutine(Fly(destination, walletView, amount));
        StartCoroutine(SizeChange(size));
    }

    public void DelayedInit(Vector3 destination, Sprite sprite, float initialSize, float size, WalletView walletView, float amount, float time)
    {
        _image.rectTransform.sizeDelta = new Vector2(initialSize, initialSize);
        StartCoroutine(SplashAnimation(destination, sprite, size, walletView, amount, time));
    }

    public void SetSprite(Sprite sprite)
    {
        _image.sprite = sprite;
    }

    private IEnumerator Fly(Vector3 destination, WalletView walletView, float amount)
    {
        float distance = Vector3.Distance(transform.localPosition, destination);
        float speed = distance / _time; 

        while(distance > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, speed * Time.deltaTime);

            distance = Vector3.Distance(transform.localPosition, destination);
            yield return null;
        }

        walletView.ChangeViewText(amount);
        Destroy(gameObject);
    }

    private IEnumerator SizeChange(float size)
    {
        float speed = Mathf.Abs(_image.rectTransform.sizeDelta.x - size)/_time;
        float currentSize;

        while (_image.rectTransform.sizeDelta.x > size)
        {
            currentSize = Mathf.MoveTowards(_image.rectTransform.sizeDelta.x, size, speed * Time.deltaTime);
            _image.rectTransform.sizeDelta = new Vector2(currentSize, currentSize);

            yield return null;
        }
    }

    private IEnumerator SplashAnimation(Vector3 destination, Sprite sprite, float size, WalletView walletView, float amount, float time)
    {

        Vector2 position = Random.insideUnitCircle * 50 + (Vector2)transform.position;

        float distance = Vector3.Distance(position, transform.transform.position);
        float speed = distance / 0.4f;

        while (distance >= 1)
        {
            distance = Vector3.Distance(position, transform.transform.position);
            transform.transform.position = Vector3.MoveTowards(transform.transform.position, position, speed * Time.deltaTime);

            StartCoroutine(Delay(destination, sprite, size, walletView, amount, time));

            yield return null;
        }
    }

    private IEnumerator Delay(Vector3 destination, Sprite sprite, float size, WalletView walletView, float amount, float time)
    {
        yield return new WaitForSeconds(0.4f);
        Init(destination, sprite, size, walletView, amount, time);
    }
}
