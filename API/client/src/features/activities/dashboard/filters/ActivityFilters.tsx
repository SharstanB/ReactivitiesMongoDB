import { Card, CardContent, Typography, List, ListItemButton, ListItemText } from '@mui/material';
import FilterAltIcon from '@mui/icons-material/FilterAlt';

export default  function Filters() {
  const options = ['All events', "I'm going", "I'm hosting"];

  return (
    <Card sx={{ mb: 2 }}>
      <CardContent>
        <Typography variant="h6" sx={{ display: 'flex', alignItems: 'center', mb: 1, color:'primary.main' }}>
          <FilterAltIcon sx={{ mr: 1 }} /> Filters
        </Typography>
        <List>
          {options.map((option) => (
            <ListItemButton key={option}>
              <ListItemText primary={option} />
            </ListItemButton>
          ))}
        </List>
      </CardContent>
    </Card>
  );
};

