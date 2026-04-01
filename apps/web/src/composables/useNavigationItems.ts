import { Home, Folder } from "lucide-vue-next"

export const useNavigationItems = () => {
    return [
        {
            title: 'Dashboard',
            icon: Home,
            to: '/'
        },
        {
            title: 'Projects',
            icon: Folder, 
            to: '/projects'
        }
    ]
}