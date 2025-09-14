using Application.Activities.Command;
using Application.DataTransferObjects.Activity;
using FluentValidation;

namespace Application.Validators
{
    public class EditActivityValidator : BaseActivityValidator<EditActivity.Command, EditActivityDTO>
    {
        public EditActivityValidator() : base(x => x.Activity)
        {
            RuleFor(x =>x.Activity.Id).NotEmpty().WithMessage("Activity Id most not be empty");
            
        }
    }




  
}
