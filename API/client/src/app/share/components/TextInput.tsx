import { TextField, TextFieldProps } from '@mui/material';
import { Control, FieldValues, Path, useController } from 'react-hook-form'

type Props<T extends FieldValues> = {
  control: Control<T>;
  name: Path<T>;
} & Omit<TextFieldProps, 'name'>

export default function TextInput<T extends FieldValues>({ control, name, ...props }: Props<T>) {
  const { field, fieldState } = useController({ control, name });

  return (
    <TextField
      {...props}
      {...field}
      value={field.value || ''}
      fullWidth
      variant='outlined'
      error={!!fieldState.error}
      helperText={fieldState.error?.message}
    />
  )
}
