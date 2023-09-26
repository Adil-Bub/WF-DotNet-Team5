import * as yup from "yup";

export const loginObjSchema = yup.object().shape({
    employeeId: yup.string().required('Employee Id is required!'),
    password: yup.string().required('Password is required!')
})

export const registerSchema = yup.object().shape({
    password: yup.string().min(3, 'Password must be at least 3 characters').max(15, 'Password can only be atmost 15 characters').required('Password is required!'),
    confirmPassword: yup.string().required('Confirm Password is required').oneOf([yup.ref('password'), null], 'Passwords must match'),
    employeeName: yup.string().required('Name is a required field!'),
    designation: yup.string().required('Designation is a required field!'),
    department: yup.string().required('Department is a required field!'),
    gender: yup.string().required('Gender is a required field!'),
    dateOfBirth: yup.date()
    .required('Date of Birth is required')
    .test(
      'is-valid-age',
      'Age must be between 18 to 100',
      function (value) {
        const today = new Date();
        const dob = new Date(value);
        const age = today.getFullYear() - dob.getFullYear();
        const monthDiff = today.getMonth() - dob.getMonth();
        // Check if age is between 18 and 100 years
        if (
          (age > 18 || (age === 18 && monthDiff >= 0)) &&
          age <= 100
        ) {
          return true;
        }
        return false;
      }
    )
})
