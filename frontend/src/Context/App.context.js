import { createContext, useState, useContext } from "react";

const AppContext = createContext(null);

const AppProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [selectedCategory, setSelectedCategory] = useState('');
    return <AppContext.Provider value={{ selectedCategory, setSelectedCategory, user, setUser }}>
        {children}
    </AppContext.Provider>
}
const useAppContext = () => {
    return useContext(AppContext);
}
export { AppProvider, AppContext, useAppContext };
