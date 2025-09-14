import { Box , TextField , Typography, List, ListItemButton, debounce} from "@mui/material";
import axios from "axios";
import { useEffect, useMemo, useState } from "react";
import { FieldValues, Path, useController, UseControllerProps } from "react-hook-form";
import { Control } from 'react-hook-form';
import { LocationIQ } from "../../../lib/types";

type Props<T extends FieldValues> = {
    control: Control<T>;
    name: Path<T>;
    lable: string;
  } & Omit<UseControllerProps<T>, 'name'>
export default function LocationInput<T extends FieldValues>({...props} : Props<T> ) {
    const { field, fieldState } = useController({...props});
    const [loading, setLoading] = useState(false);
    const [suggestions, setSuggestions] = useState<LocationIQ[]>([]);

    const [inputValue, setInputValue] = useState(field.value || '');
    
    const locationUrl = 'https://api.locationiq.com/v1/autocomplete?key=pk.56222b886b5bc426719db0a90a91a066&limit=5&dedupe=1&';

   useEffect(() => {
     if(field.value && typeof field.value === "object"){
        setInputValue(field.value.venue || '')
     }else{
        setInputValue(field.value || '')
     }
   },[field.value]);

    const fetchSuggestions = useMemo(()=> debounce(async (query :string) => {
        if (!query || query.length < 3){
            setSuggestions([]);
            return;
        }
        setLoading(true);
        try {
            const res = await axios.get<LocationIQ[]>(`${locationUrl}q=${query}`);
            setSuggestions(res.data);
        } catch (error) {
            console.log(error);
        }finally{
            setLoading(false);
        }
    }, 500), []);

    const handleChange = async (value: string) => {
        field.onChange(value);
        await fetchSuggestions(value);
    }

   const handleSelect = (location : LocationIQ)=> {
    const city = location.address?.city || location.address?.town || location.address?.village || '';
    const venue = location.display_name;
    const latitude = location.lat;
    const longitude = location.lon;

    setInputValue(venue);
    field.onChange({city, venue, latitude, longitude});
    setSuggestions([]);
   } 
   return (
    <Box sx={{ position: 'relative' }}>
        <TextField
         {...props}
         value={inputValue}
         onChange={e => handleChange(e.target.value)}
         fullWidth
         variant="outlined"
         error={!!fieldState.error}
         helperText={fieldState.error?.message}
        ></TextField>
            {loading && <Typography sx={{my: 1}}>...Loading </Typography>}
            {suggestions.length > 0 && 
            (
                <List  sx={{ 
                    position: 'absolute',
                    width: '100%',
                    bgcolor: 'background.paper',
                    border: 1,
                    borderColor: 'divider',
                    borderRadius: 1,
                    mt: 1,
                    zIndex: 1
                }}>
                    {suggestions.map(suggestion => (
                        <ListItemButton divider 
                        key={suggestion.place_id}
                        onClick={() => {handleSelect(suggestion)}}>
                           {suggestion.display_name}
                        </ListItemButton>
                    ))}
                </List>
            )  }
    </Box>
  )
}
