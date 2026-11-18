using UnityEngine;
using UnityEngine.UI;
using static Gameplay.GeneratorController;

namespace Gameplay
{
	public class ScoreController: MonoBehaviour
	{
		internal static ScoreController Score {get; private set;}
		
		[SerializeField] Text score;
		[SerializeField] Text multiplier;
		[SerializeField] Text count;
		
		int _score;
		int _remaining;
		float _multiplier;
		int _bonus;

		void Awake() => Score = this;

		void Start()
		{
			_remaining = Generator.remaining + 1;
			count.text = _remaining.ToString();
		}
		
		internal void UpdateScore(int score = 0, bool reset = true)
		{
			if (reset) _bonus = 0; 
			else _bonus++; 
			
			_multiplier = 1 + _bonus * 0.25f;
			multiplier.text = "x " + _multiplier;
			_score += Mathf.RoundToInt(score * _multiplier);
			this.score.text = _score.ToString();
		}

		internal void UpdateCount()
		{
			_remaining--;
			count.text = _remaining.ToString();
		}
	}
}