import React from 'react';
import { Alert, Box } from '@mui/material';

interface ErrorMessageProps {
  message: string;
  onClose?: () => void;
}

const ErrorMessage: React.FC<ErrorMessageProps> = ({ message, onClose }) => {
  return (
    <Box mb={2}>
      <Alert severity="error" onClose={onClose}>
        {message}
      </Alert>
    </Box>
  );
};

export default ErrorMessage;