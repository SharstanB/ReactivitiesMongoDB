import { Control, FieldValues, Path, useController } from 'react-hook-form'
import { FormControl, FormHelperText, InputLabel, MenuItem } from '@mui/material';
import Select, { SelectProps } from '@mui/material/Select';
import { BasicListObject } from "../../../lib/types";
// import {SelectInputProps } from '@mui/mat erial/Select/SelectInput';

type Props<T extends FieldValues> = {
   items: BasicListObject[] | undefined;
   label: string;
   control: Control<T>;
   name: Path<T>;
} & Omit<SelectProps, 'name'>

export default function SelectInput<T extends FieldValues>({ control, name, ...props }: Props<T>) {
    const { field, fieldState } = useController({ control, name }); 
    return (
    <FormControl fullWidth error={!!fieldState.error}>
      <InputLabel>{props.label}</InputLabel>
      <Select 
        {...field}
        value={field.value || ''}
        label={props.label}
        error={!!fieldState.error}>
        {props.items?.map(item => (
            <MenuItem key={item.id} value={item.id}>
                {item.name}
            </MenuItem>
         ))}
      </Select>
      <FormHelperText>{fieldState.error?.message}</FormHelperText>
    </FormControl>
  )
}
