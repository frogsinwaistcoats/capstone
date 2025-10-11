using UnityEngine;
using Yarn.Unity;

public class PortraitOptionsListView : OptionsListView
{
    [SerializeField] private PortraitLineView portraitLineView;

    public override void RunOptions(DialogueOption[] options, System.Action<int> onOptionSelected)
    {
        if (portraitLineView != null)
        {
            portraitLineView.DimLeftPortrait();
        }

        base.RunOptions(options, index =>
        {
            if (portraitLineView != null)
            {
                portraitLineView.ResetPortraits();
            }
            onOptionSelected?.Invoke(index);
        });
    }
}
