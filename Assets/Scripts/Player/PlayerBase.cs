using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class PlayerBase : MonoBehaviour
{
    private int _playerScore;

    protected float _moveSpeed = 2f;

    [Header("Managers")]
    [Space(20)]
    [SerializeField] protected ScoreManager _scoreManager;
    [SerializeField] protected UIManager _uiManager;

    [Header("Interactions")]
    [Space(20)]
    [SerializeField] protected ChoppingBoard _choppingBoard;
    [SerializeField] protected SidePlate _sidePlate;
    [SerializeField] protected Table _table;
    [SerializeField] protected Trash _trashCan;
    [SerializeField] protected Veggie _veggie;

    [Header("Buttons")]
    [Space(20)]
    [SerializeField] protected KeyCode _interactKey;//Button used by each player to interact
    [SerializeField] protected KeyCode _pickupFromChoppingBoardKey;//Button used by each player to pickup from the chopping board

    [Header("UI")]
    [Space(20)]
    [SerializeField] private TMP_Text _veggie1Text;//Displays the first veggie picked up
    [SerializeField] private TMP_Text _veggie2Text;//Displays the second veggie picked up
    [SerializeField] private Image _platePickedUpImage;//Shows whether or not player has pickeup up plate
    [SerializeField] private Slider _waitingSlider;//Shows whether or not player has pickeup up plate


    protected Queue<Veggie> _veggiesPickedUp;//Unprepared Veggies picked up side tables
    protected Stack<VeggieType> _veggiesPrepared;//Prepared Veggied picked up from chopping board

    public bool CanPlay { get; set; }

    public int PlayerScore
    {
        get => _playerScore;

        set
        {
            _playerScore = value;
            UpdateScoreText();
        } 
    }

    private void Awake()
    {
        CanPlay = true;

        _playerScore = 0;

        _veggiesPickedUp = new Queue<Veggie>();
        _veggiesPrepared = new Stack<VeggieType>();
    }

    private void Update()
    {
        if (!CanPlay) return;

        MovePlayer();
        Interact();
    }

    public virtual void MovePlayer()
    {
        //Implement in player child class
    }

    public virtual void UpdateScoreText()
    {
        //Implement in player child class
    }
    public virtual void UpdateTimerText(int timeLeft)
    {
        //Implement in player child class
    }

    //Return plate prepared
    public Stack<VeggieType> GetVeggiePlate()
    {
        return _veggiesPrepared;
    }


    #region Bonus
    public void AddBonusScore()
    {
        PlayerScore = _scoreManager.AddPlayerScore(PlayerScore, 10);
    }
    public void IncreaseSpeed(float tempSpeed, float time)
    {
        StartCoroutine(TemporarySpeedIncrease());

        IEnumerator TemporarySpeedIncrease()
        {
            float tempHolder = _moveSpeed;//Store default speed for a while

            _moveSpeed = tempSpeed;//Increase speed

            yield return new WaitForSeconds(time);

            _moveSpeed = tempHolder;//Set default speed back after time
        }
    }
    #endregion

    #region Score
    public virtual void DeductPointsAllPlayers()
    {
        PlayerScore = _scoreManager.DeductPlayerScore(PlayerScore, 10);
    }

    public void DeductPlayerScore()
    {
        PlayerScore = _scoreManager.DeductPlayerScore(PlayerScore, 10);
    }
    public void AddPlayerScore()
    {
        PlayerScore = _scoreManager.AddPlayerScore(PlayerScore, 20);
    }
    #endregion

    #region CollisionDetection
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //is in range to pick up veggie
        if (collider.TryGetComponent<Veggie>(out _veggie))
        {
            Debug.Log($"{_veggie.GetVeggieType()} in range");
        }

        //is in range of chopping board
        if (collider.TryGetComponent<ChoppingBoard>(out _choppingBoard))
        {
            Debug.Log($"{_choppingBoard.name} in range");
        }

        //is in range of trash can
        if (collider.TryGetComponent<Trash>(out _trashCan))
        {
            Debug.Log($"{_trashCan.name} in range");
        }

        //is in range of side plate
        if (collider.TryGetComponent<SidePlate>(out _sidePlate))
        {
            Debug.Log($"{_sidePlate.name} in range");
        }

        //is in range of table
        if (collider.TryGetComponent<Table>(out _table))
        {
            Debug.Log($"{_table.name} in range");
        }
    }


    private void OnTriggerExit2D(Collider2D collider)
    {
        //No longer in range of veggie
        if (collider.TryGetComponent<Veggie>(out _veggie))
        {
            _veggie = null;
        }
        //no longer range of chopping board
        if (collider.TryGetComponent<ChoppingBoard>(out _choppingBoard))
        {
            Debug.Log($"{_choppingBoard.name} no longer in range");
            _choppingBoard = null;
        }
        //no longer in range of trash can
        if (collider.TryGetComponent<Trash>(out _trashCan))
        {
            Debug.Log($"{_trashCan.name} no longer in range");
            _trashCan = null;
        }
        //no longer in range of side plate
        if (collider.TryGetComponent<SidePlate>(out _sidePlate))
        {
            Debug.Log($"{_sidePlate.name} no longer in range");
            _sidePlate = null;
        }
        //no longer in range of table
        if (collider.TryGetComponent<Table>(out _table))
        {
            Debug.Log($"{_table.name} no longer in range");
            _table = null;
        }
    }
    #endregion

    public virtual void Interact()
    {
        if (Input.GetKeyDown(_interactKey))
        {
            if (_veggie) InteractWithVegies();
            if (_choppingBoard) InteractWithChoppingBoard();
            if (_trashCan) InteractWithTrashCan();
            if (_sidePlate) InteractWithSidePlate();
            if (_table) InteractWithTable();
        }
        if (Input.GetKeyDown(_pickupFromChoppingBoardKey))
        {
            if (_choppingBoard) PickupFromChoppingBoard();
        }
    }


    #region Interactions
    private void InteractWithTable()
    {
        if (_veggiesPrepared.Count <= 0) return;

        _table.SetPlateForCustomer(this);
         
        //Clear plate for next service
        _veggiesPrepared.Clear();
        _platePickedUpImage.gameObject.SetActive(false);//Hide plate ui
    }

    private void InteractWithSidePlate()
    {
        bool playerHasFullHands = (_veggiesPickedUp.Count == 1 && _veggiesPrepared.Count > 0) || (_veggiesPickedUp.Count == 2);

        //if plate has veggie and player has no space to pickup veggie, ignore
        if (_sidePlate.HasVeggieOnPlate && playerHasFullHands) return;

        //if plate has veggie and player has space to pickup veggie, pickup
        if (_sidePlate.HasVeggieOnPlate && !playerHasFullHands)
        {
            _veggiesPickedUp.Enqueue(_sidePlate.PickupVeggieFromPlate());
            SetVegetableText(_veggiesPickedUp.Last().GetVeggieType());
            return;
        }

        //if plate is empty and player has veggie, drop veggie on plate
        if (!_sidePlate.HasVeggieOnPlate && _veggiesPickedUp.Count > 0)
        {
            _sidePlate.DropVeggieOnPlate(_veggiesPickedUp.Dequeue());
            ClearVeggieText();
            return;
        } 
    }

    private void ClearVeggieText()
    {
        if (_veggie1Text.text != String.Empty)//Space 1 is occupied
        {
            _veggie1Text.text = String.Empty;
            return;
        }
        if (_veggie2Text.text != String.Empty)//Space 2 is occupied
        {
            _veggie2Text.text = String.Empty;
            return;
        }
    }

    private void InteractWithTrashCan()
    {
        if (_veggiesPrepared.Count <= 0) return;

        foreach (var item in _veggiesPrepared)
        {
            Debug.Log($"Veggie {item} has been trashed");
        }

        //Trash the plate
        _veggiesPrepared.Clear();
        _platePickedUpImage.gameObject.SetActive(false);//Hide plate ui

        //Deduct points
        _playerScore = _scoreManager.DeductPlayerScore(_playerScore, 2);
    }

    private void PickupFromChoppingBoard()
    {
        //Player cannot pickup from board if hands are full with veggies or already has a plate in hand
        if (_veggiesPickedUp.Count >= 2 || _veggiesPrepared.Count > 0) return;

        _platePickedUpImage.gameObject.SetActive(true);//Show picked plate UI
        _veggiesPrepared = _choppingBoard.RemoveVeggiesFromBoard();
    }

    private void InteractWithChoppingBoard()
    {
        //Do nothing if player has no veggie in hand, chopping board in use, or chopping board is full
        if (_veggiesPickedUp.Count == 0 || _choppingBoard.IsChoppingBoardBusy || _choppingBoard.IsChoppingBoardFull) return;

        StartCoroutine(DelayPlayerWhileChopping());
        _choppingBoard.AddVeggieToBoard(_veggiesPickedUp.Dequeue());

        ClearVeggieText();

        IEnumerator DelayPlayerWhileChopping()
        {
            _waitingSlider.gameObject.SetActive(true);//Show waiting slider

            _choppingBoard.IsChoppingBoardBusy = true;
            _moveSpeed = 0;//Stop player from moving

            float timer = 0f;
            float waitingTime = 1f;//Waiting time while using the chopping board

            while (timer <= 1f)
            {
                _waitingSlider.value = timer;
                timer += Time.deltaTime;
                yield return null;
            }

            _waitingSlider.gameObject.SetActive(false);//hide waiting slider

            _moveSpeed = 2f;//Allow player to move
            _choppingBoard.IsChoppingBoardBusy = false;
        }
    }

    private void InteractWithVegies()
    {
        //Allow the player pick veggie if available, has less than two, and is not same veggie
        if (_veggiesPickedUp.Count >= 2) return;

        if (_veggiesPickedUp.Count == 1 && _veggiesPrepared.Count > 0) return;// can only hold one veggie if plate is in other hand

        if (_veggiesPickedUp.Count == 0 || _veggiesPickedUp.Last() != _veggie)
        {
            _veggiesPickedUp.Enqueue(_veggie);
            _veggie = null;

            //Set Text for vegetables
            SetVegetableText(_veggiesPickedUp.Last().GetVeggieType());
        }
        Debug.Log($"{_veggiesPickedUp.Count} elements in the queue");
    }
    #endregion
    private void SetVegetableText(VeggieType veggieType)
    {
        if (_veggie1Text.text == String.Empty)//Space 1 is free
        {
            _veggie1Text.text = veggieType.ToString();
            return;
        }
        if (_veggie2Text.text == String.Empty)//Space 2 is free
        {
            _veggie2Text.text = veggieType.ToString();
            return;
        }
    }
}
