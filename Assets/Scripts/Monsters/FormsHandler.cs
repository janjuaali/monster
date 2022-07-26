using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormsHandler : MonoBehaviour
{
    [SerializeField] private List<Form> _forms;

    private int _counter = 0;

    public Animator CurrentFormAnimator => CurrentForm.FormAnimator;
    public Form CurrentForm { get; private set; }

    public event Action FormChanged;

    private void Awake()
    {
        if (_forms.Count<=0)
            _forms = GetComponentsInChildren<Form>().ToList();       

        EnableInitialForm();
    }

    public void SetForm(Form form)
    {
        _forms.Add(form);
        EnableInitialForm();
    }

    public bool TryEnableNextForm()
    {
        _counter++;
        
        if (_counter > _forms.Count - 1)
            return false;

        if (CurrentForm != null)
            CurrentForm.gameObject.SetActive(false);

        SetCurrentForm(_counter);
        FormChanged?.Invoke();

        return true;
    }

    public void EnablePreviousForm()
    {
        _counter--;

        if (_counter <= 0)
        {
            _counter = 0;
            return;
        }

        if (CurrentForm != null)
            CurrentForm.gameObject.SetActive(false);

        SetCurrentForm(_counter);
    }

    public void SetCurrentForm(int index)
    {
        if (index > _forms.Count - 1)
            index = _forms.Count - 1;

        CurrentForm = _forms[index];

        CurrentForm.gameObject.SetActive(true);
    }

    public void EnableFirstForm()
    {
        for (int i = 0; i < _forms.Count; i++)
        {
            if (i == 0)
                SetCurrentForm(i);
            else
                _forms[i].gameObject.SetActive(false);
        }
    }

    private void EnableInitialForm()
    {
        for (int i = 0; i < _forms.Count; i++)
        {
            if (i == _counter)
                SetCurrentForm(i);
            else
                _forms[i].gameObject.SetActive(false);
        }
    }
}
