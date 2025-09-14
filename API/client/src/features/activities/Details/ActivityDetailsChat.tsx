import { Avatar, Box, Card, Paper, TextField, Typography } from "@mui/material";

export default function ActivityDetailsChat() {
  return (
    <Card style={{ margin: "20px auto", position: "relative", color: "#fff" }} >
      <Box bgcolor="primary.main" color="primary.contrastText" p={2} textAlign="center">
        <Typography variant="h6">Chat about this event</Typography>
      </Box>

      <Box margin={2} >
        <TextField
          fullWidth
          variant="outlined"
          placeholder="Enter your comment (Enter to submit, SHIFT + Enter for new line)"
          multiline
          rows={4}
        />
      </Box>
      <Box mt={2}>
        <Paper elevation={1} style={{ padding: '16px', display: 'flex', alignItems: 'center' }}>
          <Avatar style={{ marginRight: '8px' }}>B</Avatar>
          <Box sx={{marginLeft:'10px'}}>
            <Typography variant="body2" color="textSecondary">Bob</Typography>
            <Typography variant="caption" color="textSecondary">2 hours ago</Typography>
            <Typography variant="body1">Comment goes here</Typography>
          </Box>
        </Paper>
      </Box>
    </Card>
  );
};

