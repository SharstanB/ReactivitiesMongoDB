import { Avatar, Box, Button, Typography } from "@mui/material";

export default function ActivityDetailsSidebar() {
  return (
    <Box
      sx={{
        border: "1px solid #ccc",
        borderRadius: 4,
        overflow: "hidden",
      }}
    >
      <Box sx={{ backgroundColor: "#1976d2", color: "#fff", padding: 2 }}>
        <Typography variant="h6">2 people going</Typography>
      </Box>
      <Box sx={{ display: "flex", justifyContent:'space-between', padding: 2 }}>
        <Box sx={{ display: "flex", alignItems: "center", padding: 2 }}>
            <Avatar sx={{ marginRight: 2 }}>B</Avatar>
            <Typography variant="body1">Bob</Typography>
        </Box>
        <Box sx={{ display: "flex", flexDirection: "column" }}>
          <Button
            variant="contained"
            color="warning"
            sx={{ margin: 'auto' }}
          >
            Host
          </Button>
          <Typography
            variant="body2"
            color="warning"
            sx={{ marginLeft: 1 }}
          >
            Following
          </Typography>
        </Box>
      </Box>
    </Box>
  );
}
