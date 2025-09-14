import { Control, FieldValues, Path, useController } from 'react-hook-form'
import { DateTimePicker, DateTimePickerProps } from '@mui/x-date-pickers/DateTimePicker';
import dayjs from 'dayjs';

type Props<T extends FieldValues> = {
  control: Control<T>;
  name: Path<T>;
} & Omit<DateTimePickerProps, 'name'>

export default function DateInput<T extends FieldValues>({ control, name, ...props }: Props<T>) {
  const { field, fieldState } = useController({ control, name });

  return (
    <DateTimePicker
    {...props}
    value={field.value ? dayjs(field.value) : null}
    onChange={(value) => field.onChange(value?.toDate())}
    sx={{ width: '100%' }}
    slotProps={{
      textField: {
        onBlur: field.onBlur,
        error: !!fieldState.error,
        helperText: fieldState.error?.message,
      },
    }}
    />        
  )
}

