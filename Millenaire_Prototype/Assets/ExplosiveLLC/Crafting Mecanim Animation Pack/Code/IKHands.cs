using UnityEngine;

public class IKHands : MonoBehaviour
{
	public Transform leftHandObj;
	public Transform rightHandObj;
	public Transform attachLeft;
	public Transform attachRight;

	[Range(0, 1)] public float leftHandPositionWeight;
	[Range(0, 1)] public float leftHandRotationWeight;
	[Range(0, 1)] public float rightHandPositionWeight;
	[Range(0, 1)] public float rightHandRotationWeight;

	public bool canBeUsed;

	private Animator animator;
	
	void Start()
	{
		animator = gameObject.GetComponent<Animator>();
	}
	
	void OnAnimatorIK(int layerIndex)
	{
		if(leftHandObj != null)
		{
			animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,leftHandPositionWeight);
			animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,leftHandRotationWeight);
			if(attachLeft)
			{
				animator.SetIKPosition(AvatarIKGoal.LeftHand,attachLeft.position);                    
				animator.SetIKRotation(AvatarIKGoal.LeftHand,attachLeft.rotation);
			}
		}
		if(rightHandObj != null)
		{
			animator.SetIKPositionWeight(AvatarIKGoal.RightHand,rightHandPositionWeight);
			animator.SetIKRotationWeight(AvatarIKGoal.RightHand,rightHandRotationWeight);
			if(attachRight)
			{
				animator.SetIKPosition(AvatarIKGoal.RightHand,attachRight.position);                    
				animator.SetIKRotation(AvatarIKGoal.RightHand,attachRight.rotation);
			}
		}
	}
}